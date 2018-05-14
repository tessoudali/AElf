// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ProtoAccount.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AElf.Serialization.ProtoMessages {

  /// <summary>Holder for reflection information generated from ProtoAccount.proto</summary>
  public static partial class ProtoAccountReflection {

    #region Descriptor
    /// <summary>File descriptor for ProtoAccount.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProtoAccountReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJQcm90b0FjY291bnQucHJvdG8iHwoMUHJvdG9BY2NvdW50Eg8KB2FkZHJl",
            "c3MYASABKAxCI6oCIEFFbGYuU2VyaWFsaXphdGlvbi5Qcm90b01lc3NhZ2Vz",
            "YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AElf.Serialization.ProtoMessages.ProtoAccount), global::AElf.Serialization.ProtoMessages.ProtoAccount.Parser, new[]{ "Address" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ProtoAccount : pb::IMessage<ProtoAccount> {
    private static readonly pb::MessageParser<ProtoAccount> _parser = new pb::MessageParser<ProtoAccount>(() => new ProtoAccount());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ProtoAccount> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AElf.Serialization.ProtoMessages.ProtoAccountReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ProtoAccount() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ProtoAccount(ProtoAccount other) : this() {
      address_ = other.address_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ProtoAccount Clone() {
      return new ProtoAccount(this);
    }

    /// <summary>Field number for the "address" field.</summary>
    public const int AddressFieldNumber = 1;
    private pb::ByteString address_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Address {
      get { return address_; }
      set {
        address_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ProtoAccount);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ProtoAccount other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Address != other.Address) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Address.Length != 0) hash ^= Address.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Address.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(Address);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Address.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Address);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ProtoAccount other) {
      if (other == null) {
        return;
      }
      if (other.Address.Length != 0) {
        Address = other.Address;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Address = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code