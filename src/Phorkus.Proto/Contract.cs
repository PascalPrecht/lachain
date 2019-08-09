// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: contract.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Phorkus.Proto {

  /// <summary>Holder for reflection information generated from contract.proto</summary>
  public static partial class ContractReflection {

    #region Descriptor
    /// <summary>File descriptor for contract.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ContractReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5jb250cmFjdC5wcm90bxoNZGVmYXVsdC5wcm90byJBCghDb250cmFjdBIi",
            "ChBjb250cmFjdF9hZGRyZXNzGAEgASgLMgguVUludDE2MBIRCglieXRlX2Nv",
            "ZGUYAyABKAxCIwoRY29tLmxhdG9rZW4ucHJvdG+qAg1QaG9ya3VzLlByb3Rv",
            "YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Phorkus.Proto.DefaultReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Proto.Contract), global::Phorkus.Proto.Contract.Parser, new[]{ "ContractAddress", "ByteCode" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Contract : pb::IMessage<Contract> {
    private static readonly pb::MessageParser<Contract> _parser = new pb::MessageParser<Contract>(() => new Contract());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Contract> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Proto.ContractReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Contract() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Contract(Contract other) : this() {
      ContractAddress = other.contractAddress_ != null ? other.ContractAddress.Clone() : null;
      byteCode_ = other.byteCode_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Contract Clone() {
      return new Contract(this);
    }

    /// <summary>Field number for the "contract_address" field.</summary>
    public const int ContractAddressFieldNumber = 1;
    private global::Phorkus.Proto.UInt160 contractAddress_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt160 ContractAddress {
      get { return contractAddress_; }
      set {
        contractAddress_ = value;
      }
    }

    /// <summary>Field number for the "byte_code" field.</summary>
    public const int ByteCodeFieldNumber = 3;
    private pb::ByteString byteCode_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString ByteCode {
      get { return byteCode_; }
      set {
        byteCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Contract);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Contract other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(ContractAddress, other.ContractAddress)) return false;
      if (ByteCode != other.ByteCode) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (contractAddress_ != null) hash ^= ContractAddress.GetHashCode();
      if (ByteCode.Length != 0) hash ^= ByteCode.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (contractAddress_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(ContractAddress);
      }
      if (ByteCode.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(ByteCode);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (contractAddress_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ContractAddress);
      }
      if (ByteCode.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(ByteCode);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Contract other) {
      if (other == null) {
        return;
      }
      if (other.contractAddress_ != null) {
        if (contractAddress_ == null) {
          contractAddress_ = new global::Phorkus.Proto.UInt160();
        }
        ContractAddress.MergeFrom(other.ContractAddress);
      }
      if (other.ByteCode.Length != 0) {
        ByteCode = other.ByteCode;
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
            if (contractAddress_ == null) {
              contractAddress_ = new global::Phorkus.Proto.UInt160();
            }
            input.ReadMessage(contractAddress_);
            break;
          }
          case 26: {
            ByteCode = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
