// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: transaction.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Phorkus.Proto {

  /// <summary>Holder for reflection information generated from transaction.proto</summary>
  public static partial class TransactionReflection {

    #region Descriptor
    /// <summary>File descriptor for transaction.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TransactionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChF0cmFuc2FjdGlvbi5wcm90bxoNZGVmYXVsdC5wcm90byK+AQoLVHJhbnNh",
            "Y3Rpb24SHgoEdHlwZRgBIAEoDjIQLlRyYW5zYWN0aW9uVHlwZRIUCgJ0bxgC",
            "IAEoCzIILlVJbnQxNjASEgoKaW52b2NhdGlvbhgDIAEoDBIXCgV2YWx1ZRgE",
            "IAEoCzIILlVJbnQyNTYSDgoGZGVwbG95GAUgASgMEhYKBGZyb20YBiABKAsy",
            "CC5VSW50MTYwEg0KBW5vbmNlGAcgASgEEhUKA2ZlZRgIIAEoCzIILlVJbnQy",
            "NTYirAEKE0FjY2VwdGVkVHJhbnNhY3Rpb24SIQoLdHJhbnNhY3Rpb24YASAB",
            "KAsyDC5UcmFuc2FjdGlvbhIWCgRoYXNoGAIgASgLMgguVUludDI1NhIdCglz",
            "aWduYXR1cmUYAyABKAsyCi5TaWduYXR1cmUSFwoFYmxvY2sYBCABKAsyCC5V",
            "SW50MjU2EiIKBnN0YXR1cxgFIAEoDjISLlRyYW5zYWN0aW9uU3RhdHVzKpAB",
            "ChFUcmFuc2FjdGlvblN0YXR1cxIeChpUUkFOU0FDVElPTl9TVEFUVVNfVU5L",
            "Tk9XThAAEhsKF1RSQU5TQUNUSU9OX1NUQVRVU19QT09MEAESHwobVFJBTlNB",
            "Q1RJT05fU1RBVFVTX0VYRUNVVEVEEAISHQoZVFJBTlNBQ1RJT05fU1RBVFVT",
            "X0ZBSUxFRBADKmsKD1RyYW5zYWN0aW9uVHlwZRIcChhUUkFOU0FDVElPTl9U",
            "WVBFX1VOS05PV04QABIdChlUUkFOU0FDVElPTl9UWVBFX1RSQU5TRkVSEAES",
            "GwoXVFJBTlNBQ1RJT05fVFlQRV9ERVBMT1kQAkIjChFjb20ubGF0b2tlbi5w",
            "cm90b6oCDVBob3JrdXMuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Phorkus.Proto.DefaultReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Phorkus.Proto.TransactionStatus), typeof(global::Phorkus.Proto.TransactionType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Proto.Transaction), global::Phorkus.Proto.Transaction.Parser, new[]{ "Type", "To", "Invocation", "Value", "Deploy", "From", "Nonce", "Fee" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Proto.AcceptedTransaction), global::Phorkus.Proto.AcceptedTransaction.Parser, new[]{ "Transaction", "Hash", "Signature", "Block", "Status" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum TransactionStatus {
    [pbr::OriginalName("TRANSACTION_STATUS_UNKNOWN")] Unknown = 0,
    [pbr::OriginalName("TRANSACTION_STATUS_POOL")] Pool = 1,
    [pbr::OriginalName("TRANSACTION_STATUS_EXECUTED")] Executed = 2,
    [pbr::OriginalName("TRANSACTION_STATUS_FAILED")] Failed = 3,
  }

  public enum TransactionType {
    [pbr::OriginalName("TRANSACTION_TYPE_UNKNOWN")] Unknown = 0,
    [pbr::OriginalName("TRANSACTION_TYPE_TRANSFER")] Transfer = 1,
    [pbr::OriginalName("TRANSACTION_TYPE_DEPLOY")] Deploy = 2,
  }

  #endregion

  #region Messages
  public sealed partial class Transaction : pb::IMessage<Transaction> {
    private static readonly pb::MessageParser<Transaction> _parser = new pb::MessageParser<Transaction>(() => new Transaction());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Transaction> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Proto.TransactionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Transaction() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Transaction(Transaction other) : this() {
      type_ = other.type_;
      to_ = other.to_ != null ? other.to_.Clone() : null;
      invocation_ = other.invocation_;
      value_ = other.value_ != null ? other.value_.Clone() : null;
      deploy_ = other.deploy_;
      from_ = other.from_ != null ? other.from_.Clone() : null;
      nonce_ = other.nonce_;
      fee_ = other.fee_ != null ? other.fee_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Transaction Clone() {
      return new Transaction(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Phorkus.Proto.TransactionType type_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.TransactionType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "to" field.</summary>
    public const int ToFieldNumber = 2;
    private global::Phorkus.Proto.UInt160 to_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt160 To {
      get { return to_; }
      set {
        to_ = value;
      }
    }

    /// <summary>Field number for the "invocation" field.</summary>
    public const int InvocationFieldNumber = 3;
    private pb::ByteString invocation_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Invocation {
      get { return invocation_; }
      set {
        invocation_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 4;
    private global::Phorkus.Proto.UInt256 value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt256 Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    /// <summary>Field number for the "deploy" field.</summary>
    public const int DeployFieldNumber = 5;
    private pb::ByteString deploy_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Deploy {
      get { return deploy_; }
      set {
        deploy_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "from" field.</summary>
    public const int FromFieldNumber = 6;
    private global::Phorkus.Proto.UInt160 from_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt160 From {
      get { return from_; }
      set {
        from_ = value;
      }
    }

    /// <summary>Field number for the "nonce" field.</summary>
    public const int NonceFieldNumber = 7;
    private ulong nonce_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Nonce {
      get { return nonce_; }
      set {
        nonce_ = value;
      }
    }

    /// <summary>Field number for the "fee" field.</summary>
    public const int FeeFieldNumber = 8;
    private global::Phorkus.Proto.UInt256 fee_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt256 Fee {
      get { return fee_; }
      set {
        fee_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Transaction);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Transaction other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (!object.Equals(To, other.To)) return false;
      if (Invocation != other.Invocation) return false;
      if (!object.Equals(Value, other.Value)) return false;
      if (Deploy != other.Deploy) return false;
      if (!object.Equals(From, other.From)) return false;
      if (Nonce != other.Nonce) return false;
      if (!object.Equals(Fee, other.Fee)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != 0) hash ^= Type.GetHashCode();
      if (to_ != null) hash ^= To.GetHashCode();
      if (Invocation.Length != 0) hash ^= Invocation.GetHashCode();
      if (value_ != null) hash ^= Value.GetHashCode();
      if (Deploy.Length != 0) hash ^= Deploy.GetHashCode();
      if (from_ != null) hash ^= From.GetHashCode();
      if (Nonce != 0UL) hash ^= Nonce.GetHashCode();
      if (fee_ != null) hash ^= Fee.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Type != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (to_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(To);
      }
      if (Invocation.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(Invocation);
      }
      if (value_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Value);
      }
      if (Deploy.Length != 0) {
        output.WriteRawTag(42);
        output.WriteBytes(Deploy);
      }
      if (from_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(From);
      }
      if (Nonce != 0UL) {
        output.WriteRawTag(56);
        output.WriteUInt64(Nonce);
      }
      if (fee_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(Fee);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (to_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(To);
      }
      if (Invocation.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Invocation);
      }
      if (value_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Value);
      }
      if (Deploy.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Deploy);
      }
      if (from_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(From);
      }
      if (Nonce != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Nonce);
      }
      if (fee_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Fee);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Transaction other) {
      if (other == null) {
        return;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.to_ != null) {
        if (to_ == null) {
          to_ = new global::Phorkus.Proto.UInt160();
        }
        To.MergeFrom(other.To);
      }
      if (other.Invocation.Length != 0) {
        Invocation = other.Invocation;
      }
      if (other.value_ != null) {
        if (value_ == null) {
          value_ = new global::Phorkus.Proto.UInt256();
        }
        Value.MergeFrom(other.Value);
      }
      if (other.Deploy.Length != 0) {
        Deploy = other.Deploy;
      }
      if (other.from_ != null) {
        if (from_ == null) {
          from_ = new global::Phorkus.Proto.UInt160();
        }
        From.MergeFrom(other.From);
      }
      if (other.Nonce != 0UL) {
        Nonce = other.Nonce;
      }
      if (other.fee_ != null) {
        if (fee_ == null) {
          fee_ = new global::Phorkus.Proto.UInt256();
        }
        Fee.MergeFrom(other.Fee);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            type_ = (global::Phorkus.Proto.TransactionType) input.ReadEnum();
            break;
          }
          case 18: {
            if (to_ == null) {
              to_ = new global::Phorkus.Proto.UInt160();
            }
            input.ReadMessage(to_);
            break;
          }
          case 26: {
            Invocation = input.ReadBytes();
            break;
          }
          case 34: {
            if (value_ == null) {
              value_ = new global::Phorkus.Proto.UInt256();
            }
            input.ReadMessage(value_);
            break;
          }
          case 42: {
            Deploy = input.ReadBytes();
            break;
          }
          case 50: {
            if (from_ == null) {
              from_ = new global::Phorkus.Proto.UInt160();
            }
            input.ReadMessage(from_);
            break;
          }
          case 56: {
            Nonce = input.ReadUInt64();
            break;
          }
          case 66: {
            if (fee_ == null) {
              fee_ = new global::Phorkus.Proto.UInt256();
            }
            input.ReadMessage(fee_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class AcceptedTransaction : pb::IMessage<AcceptedTransaction> {
    private static readonly pb::MessageParser<AcceptedTransaction> _parser = new pb::MessageParser<AcceptedTransaction>(() => new AcceptedTransaction());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AcceptedTransaction> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Proto.TransactionReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AcceptedTransaction() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AcceptedTransaction(AcceptedTransaction other) : this() {
      transaction_ = other.transaction_ != null ? other.transaction_.Clone() : null;
      hash_ = other.hash_ != null ? other.hash_.Clone() : null;
      signature_ = other.signature_ != null ? other.signature_.Clone() : null;
      block_ = other.block_ != null ? other.block_.Clone() : null;
      status_ = other.status_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AcceptedTransaction Clone() {
      return new AcceptedTransaction(this);
    }

    /// <summary>Field number for the "transaction" field.</summary>
    public const int TransactionFieldNumber = 1;
    private global::Phorkus.Proto.Transaction transaction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.Transaction Transaction {
      get { return transaction_; }
      set {
        transaction_ = value;
      }
    }

    /// <summary>Field number for the "hash" field.</summary>
    public const int HashFieldNumber = 2;
    private global::Phorkus.Proto.UInt256 hash_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt256 Hash {
      get { return hash_; }
      set {
        hash_ = value;
      }
    }

    /// <summary>Field number for the "signature" field.</summary>
    public const int SignatureFieldNumber = 3;
    private global::Phorkus.Proto.Signature signature_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.Signature Signature {
      get { return signature_; }
      set {
        signature_ = value;
      }
    }

    /// <summary>Field number for the "block" field.</summary>
    public const int BlockFieldNumber = 4;
    private global::Phorkus.Proto.UInt256 block_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.UInt256 Block {
      get { return block_; }
      set {
        block_ = value;
      }
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 5;
    private global::Phorkus.Proto.TransactionStatus status_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Proto.TransactionStatus Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AcceptedTransaction);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AcceptedTransaction other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Transaction, other.Transaction)) return false;
      if (!object.Equals(Hash, other.Hash)) return false;
      if (!object.Equals(Signature, other.Signature)) return false;
      if (!object.Equals(Block, other.Block)) return false;
      if (Status != other.Status) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (transaction_ != null) hash ^= Transaction.GetHashCode();
      if (hash_ != null) hash ^= Hash.GetHashCode();
      if (signature_ != null) hash ^= Signature.GetHashCode();
      if (block_ != null) hash ^= Block.GetHashCode();
      if (Status != 0) hash ^= Status.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (transaction_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Transaction);
      }
      if (hash_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Hash);
      }
      if (signature_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Signature);
      }
      if (block_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Block);
      }
      if (Status != 0) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (transaction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Transaction);
      }
      if (hash_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Hash);
      }
      if (signature_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Signature);
      }
      if (block_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Block);
      }
      if (Status != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AcceptedTransaction other) {
      if (other == null) {
        return;
      }
      if (other.transaction_ != null) {
        if (transaction_ == null) {
          transaction_ = new global::Phorkus.Proto.Transaction();
        }
        Transaction.MergeFrom(other.Transaction);
      }
      if (other.hash_ != null) {
        if (hash_ == null) {
          hash_ = new global::Phorkus.Proto.UInt256();
        }
        Hash.MergeFrom(other.Hash);
      }
      if (other.signature_ != null) {
        if (signature_ == null) {
          signature_ = new global::Phorkus.Proto.Signature();
        }
        Signature.MergeFrom(other.Signature);
      }
      if (other.block_ != null) {
        if (block_ == null) {
          block_ = new global::Phorkus.Proto.UInt256();
        }
        Block.MergeFrom(other.Block);
      }
      if (other.Status != 0) {
        Status = other.Status;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (transaction_ == null) {
              transaction_ = new global::Phorkus.Proto.Transaction();
            }
            input.ReadMessage(transaction_);
            break;
          }
          case 18: {
            if (hash_ == null) {
              hash_ = new global::Phorkus.Proto.UInt256();
            }
            input.ReadMessage(hash_);
            break;
          }
          case 26: {
            if (signature_ == null) {
              signature_ = new global::Phorkus.Proto.Signature();
            }
            input.ReadMessage(signature_);
            break;
          }
          case 34: {
            if (block_ == null) {
              block_ = new global::Phorkus.Proto.UInt256();
            }
            input.ReadMessage(block_);
            break;
          }
          case 40: {
            status_ = (global::Phorkus.Proto.TransactionStatus) input.ReadEnum();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
