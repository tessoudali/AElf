using System;
using AElf.Common;
using AElf.Kernel;
using AElf.Kernel.KernelAccount;
using AElf.Sdk.CSharp;
using Google.Protobuf;

namespace AElf.Contracts.Genesis
{
    public class BasicContractZero : CSharpSmartContract<BasicContractZeroState>, ISmartContractZero
    {
        #region Views

        [View]
        public ulong CurrentContractSerialNumber()
        {
            return State.ContractSerialNumber.Value;
        }

        [View]
        public string GetContractInfo(Address contractAddress)
        {
            var info = State.ContractInfos[contractAddress];
            if (info == null)
            {
                return string.Empty;
            }

            return info.ToString();
        }

        [View]
        public Address GetContractOwner(Address contractAddress)
        {
            var info = State.ContractInfos[contractAddress];
            return info?.Owner;
        }

        [View]
        public Hash GetContractHash(Address contractAddress)
        {
            var info = State.ContractInfos[contractAddress];
            return info?.CodeHash;
        }

        [View]
        public Address GetContractAddress(Hash codeHash)
        {
            return State.CodeAddressMapping[codeHash];
        }

        #endregion Views

        #region Actions

        public byte[] DeploySmartContract(int category, byte[] code)
        {
            var serialNumber = State.ContractSerialNumber.Value;
            // Increment
            State.ContractSerialNumber.Value = serialNumber + 1;
            var contractAddress = Address.BuildContractAddress(Context.ChainId, serialNumber);

            var codeHash = Hash.FromRawBytes(code);

            var address = State.CodeAddressMapping[codeHash];

            //Assert(address == null || address.Value.Length == 0, "should only deploy one smart contract once");

            State.CodeAddressMapping[codeHash] = contractAddress;

            var info = new ContractInfo
            {
                SerialNumber = serialNumber,
                Owner = Context.Sender,
                Category = category,
                CodeHash = codeHash
            };
            State.ContractInfos[contractAddress] = info;

            var reg = new SmartContractRegistration
            {
                Category = category,
                Code = ByteString.CopyFrom(code),
                CodeHash = codeHash
            };

            Context.DeployContract(contractAddress, reg);

            Context.FireEvent(new ContractHasBeenDeployed()
            {
                CodeHash = codeHash,
                Address = contractAddress,
                Creator = Context.Sender
            });

            Context.LogDebug(() => "BasicContractZero - Deployment ContractHash: " + codeHash.ToHex());
            Context.LogDebug(() => "BasicContractZero - Deployment success: " + contractAddress.GetFormatted());
            return contractAddress.DumpByteArray();
        }

        public byte[] UpdateSmartContract(Address contractAddress, byte[] code)
        {
            var info = State.ContractInfos[contractAddress];

            Assert(info != null, "Contract does not exist.");
            Assert(info.Owner.Equals(Context.Sender), "Only owner is allowed to update code.");

            var oldCodeHash = info.CodeHash;
            var newCodeHash = Hash.FromRawBytes(code);
            Assert(!oldCodeHash.Equals(newCodeHash), "Code is not changed.");

            info.CodeHash = newCodeHash;
            State.ContractInfos[contractAddress] = info;

            var reg = new SmartContractRegistration
            {
                Category = info.Category,
                Code = ByteString.CopyFrom(code),
                CodeHash = newCodeHash
            };

            Context.UpdateContract(contractAddress, reg);
            
            State.CodeAddressMapping[newCodeHash] = contractAddress;

            
            Context.FireEvent(new ContractCodeHasBeenUpdated()
            {
                Address = contractAddress,
                OldCodeHash = oldCodeHash,
                NewCodeHash = newCodeHash
            });

            Context.LogDebug(() => "BasicContractZero - update success: " + contractAddress.GetFormatted());
            return contractAddress.DumpByteArray();
        }

        public void ChangeContractOwner(Address contractAddress, Address newOwner)
        {
            var info = State.ContractInfos[contractAddress];
            Assert(info != null && info.Owner.Equals(Context.Sender), "no permission.");

            var oldOwner = info.Owner;
            info.Owner = newOwner;
            State.ContractInfos[contractAddress] = info;
            Context.FireEvent(new OwnerHasBeenChanged
            {
                Address = contractAddress,
                OldOwner = oldOwner,
                NewOwner = newOwner
            });
        }

        #endregion Actions
    }
}