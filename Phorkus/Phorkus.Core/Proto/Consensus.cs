// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: consensus.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Phorkus.Core.Proto {

  /// <summary>Holder for reflection information generated from consensus.proto</summary>
  public static partial class ConsensusReflection {

    #region Descriptor
    /// <summary>File descriptor for consensus.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ConsensusReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9jb25zZW5zdXMucHJvdG8aDWRlZmF1bHQucHJvdG8aEXRyYW5zYWN0aW9u",
            "LnByb3RvIi4KE0NvbnNlbnN1c0NoYW5nZVZpZXcSFwoPbmV3X3ZpZXdfbnVt",
            "YmVyGAEgASgNIpYBChdDb25zZW5zdXNQcmVwYXJlUmVxdWVzdBINCgVub25j",
            "ZRgBIAEoBBIkChJ0cmFuc2FjdGlvbl9oYXNoZXMYAiADKAsyCC5VSW50MjU2",
            "EicKEW1pbmVyX3RyYW5zYWN0aW9uGAMgASgLMgwuVHJhbnNhY3Rpb24SHQoJ",
            "c2lnbmF0dXJlGAQgASgLMgouU2lnbmF0dXJlIjkKGENvbnNlbnN1c1ByZXBh",
            "cmVSZXNwb25zZRIdCglzaWduYXR1cmUYASABKAsyCi5TaWduYXR1cmUirwMK",
            "GkNvbnNlbnN1c1BheWxvYWRDdXN0b21EYXRhEj4KBHR5cGUYASABKA4yMC5D",
            "b25zZW5zdXNQYXlsb2FkQ3VzdG9tRGF0YS5Db25zZW5zdXNNZXNzYWdlVHlw",
            "ZRITCgt2aWV3X251bWJlchgCIAEoDRIrCgtjaGFuZ2VfdmlldxgDIAEoCzIU",
            "LkNvbnNlbnN1c0NoYW5nZVZpZXdIABIzCg9wcmVwYXJlX3JlcXVlc3QYBCAB",
            "KAsyGC5Db25zZW5zdXNQcmVwYXJlUmVxdWVzdEgAEjUKEHByZXBhcmVfcmVz",
            "cG9uc2UYBSABKAsyGS5Db25zZW5zdXNQcmVwYXJlUmVzcG9uc2VIACKXAQoU",
            "Q29uc2Vuc3VzTWVzc2FnZVR5cGUSJgoiQ09OU0VOU1VTX01FU1NBR0VfVFlQ",
            "RV9DSEFOR0VfVklFVxAAEioKJkNPTlNFTlNVU19NRVNTQUdFX1RZUEVfUFJF",
            "UEFSRV9SRVFVRVNUECASKwonQ09OU0VOU1VTX01FU1NBR0VfVFlQRV9QUkVQ",
            "QVJFX1JFU1BPTlNFECFCCQoHbWVzc2FnZUIVqgISUGhvcmt1cy5Db3JlLlBy",
            "b3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Phorkus.Core.Proto.DefaultReflection.Descriptor, global::Phorkus.Core.Proto.TransactionReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Core.Proto.ConsensusChangeView), global::Phorkus.Core.Proto.ConsensusChangeView.Parser, new[]{ "NewViewNumber" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Core.Proto.ConsensusPrepareRequest), global::Phorkus.Core.Proto.ConsensusPrepareRequest.Parser, new[]{ "Nonce", "TransactionHashes", "MinerTransaction", "Signature" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Core.Proto.ConsensusPrepareResponse), global::Phorkus.Core.Proto.ConsensusPrepareResponse.Parser, new[]{ "Signature" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Phorkus.Core.Proto.ConsensusPayloadCustomData), global::Phorkus.Core.Proto.ConsensusPayloadCustomData.Parser, new[]{ "Type", "ViewNumber", "ChangeView", "PrepareRequest", "PrepareResponse" }, new[]{ "Message" }, new[]{ typeof(global::Phorkus.Core.Proto.ConsensusPayloadCustomData.Types.ConsensusMessageType) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ConsensusChangeView : pb::IMessage<ConsensusChangeView> {
    private static readonly pb::MessageParser<ConsensusChangeView> _parser = new pb::MessageParser<ConsensusChangeView>(() => new ConsensusChangeView());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConsensusChangeView> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Core.Proto.ConsensusReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusChangeView() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusChangeView(ConsensusChangeView other) : this() {
      newViewNumber_ = other.newViewNumber_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusChangeView Clone() {
      return new ConsensusChangeView(this);
    }

    /// <summary>Field number for the "new_view_number" field.</summary>
    public const int NewViewNumberFieldNumber = 1;
    private uint newViewNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint NewViewNumber {
      get { return newViewNumber_; }
      set {
        newViewNumber_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConsensusChangeView);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConsensusChangeView other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (NewViewNumber != other.NewViewNumber) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (NewViewNumber != 0) hash ^= NewViewNumber.GetHashCode();
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
      if (NewViewNumber != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(NewViewNumber);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (NewViewNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(NewViewNumber);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConsensusChangeView other) {
      if (other == null) {
        return;
      }
      if (other.NewViewNumber != 0) {
        NewViewNumber = other.NewViewNumber;
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
            NewViewNumber = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class ConsensusPrepareRequest : pb::IMessage<ConsensusPrepareRequest> {
    private static readonly pb::MessageParser<ConsensusPrepareRequest> _parser = new pb::MessageParser<ConsensusPrepareRequest>(() => new ConsensusPrepareRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConsensusPrepareRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Core.Proto.ConsensusReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPrepareRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPrepareRequest(ConsensusPrepareRequest other) : this() {
      nonce_ = other.nonce_;
      transactionHashes_ = other.transactionHashes_.Clone();
      minerTransaction_ = other.minerTransaction_ != null ? other.minerTransaction_.Clone() : null;
      signature_ = other.signature_ != null ? other.signature_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPrepareRequest Clone() {
      return new ConsensusPrepareRequest(this);
    }

    /// <summary>Field number for the "nonce" field.</summary>
    public const int NonceFieldNumber = 1;
    private ulong nonce_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Nonce {
      get { return nonce_; }
      set {
        nonce_ = value;
      }
    }

    /// <summary>Field number for the "transaction_hashes" field.</summary>
    public const int TransactionHashesFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Phorkus.Core.Proto.UInt256> _repeated_transactionHashes_codec
        = pb::FieldCodec.ForMessage(18, global::Phorkus.Core.Proto.UInt256.Parser);
    private readonly pbc::RepeatedField<global::Phorkus.Core.Proto.UInt256> transactionHashes_ = new pbc::RepeatedField<global::Phorkus.Core.Proto.UInt256>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Phorkus.Core.Proto.UInt256> TransactionHashes {
      get { return transactionHashes_; }
    }

    /// <summary>Field number for the "miner_transaction" field.</summary>
    public const int MinerTransactionFieldNumber = 3;
    private global::Phorkus.Core.Proto.Transaction minerTransaction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.Transaction MinerTransaction {
      get { return minerTransaction_; }
      set {
        minerTransaction_ = value;
      }
    }

    /// <summary>Field number for the "signature" field.</summary>
    public const int SignatureFieldNumber = 4;
    private global::Phorkus.Core.Proto.Signature signature_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.Signature Signature {
      get { return signature_; }
      set {
        signature_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConsensusPrepareRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConsensusPrepareRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Nonce != other.Nonce) return false;
      if(!transactionHashes_.Equals(other.transactionHashes_)) return false;
      if (!object.Equals(MinerTransaction, other.MinerTransaction)) return false;
      if (!object.Equals(Signature, other.Signature)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Nonce != 0UL) hash ^= Nonce.GetHashCode();
      hash ^= transactionHashes_.GetHashCode();
      if (minerTransaction_ != null) hash ^= MinerTransaction.GetHashCode();
      if (signature_ != null) hash ^= Signature.GetHashCode();
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
      if (Nonce != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Nonce);
      }
      transactionHashes_.WriteTo(output, _repeated_transactionHashes_codec);
      if (minerTransaction_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(MinerTransaction);
      }
      if (signature_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Signature);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Nonce != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Nonce);
      }
      size += transactionHashes_.CalculateSize(_repeated_transactionHashes_codec);
      if (minerTransaction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(MinerTransaction);
      }
      if (signature_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Signature);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConsensusPrepareRequest other) {
      if (other == null) {
        return;
      }
      if (other.Nonce != 0UL) {
        Nonce = other.Nonce;
      }
      transactionHashes_.Add(other.transactionHashes_);
      if (other.minerTransaction_ != null) {
        if (minerTransaction_ == null) {
          minerTransaction_ = new global::Phorkus.Core.Proto.Transaction();
        }
        MinerTransaction.MergeFrom(other.MinerTransaction);
      }
      if (other.signature_ != null) {
        if (signature_ == null) {
          signature_ = new global::Phorkus.Core.Proto.Signature();
        }
        Signature.MergeFrom(other.Signature);
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
            Nonce = input.ReadUInt64();
            break;
          }
          case 18: {
            transactionHashes_.AddEntriesFrom(input, _repeated_transactionHashes_codec);
            break;
          }
          case 26: {
            if (minerTransaction_ == null) {
              minerTransaction_ = new global::Phorkus.Core.Proto.Transaction();
            }
            input.ReadMessage(minerTransaction_);
            break;
          }
          case 34: {
            if (signature_ == null) {
              signature_ = new global::Phorkus.Core.Proto.Signature();
            }
            input.ReadMessage(signature_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class ConsensusPrepareResponse : pb::IMessage<ConsensusPrepareResponse> {
    private static readonly pb::MessageParser<ConsensusPrepareResponse> _parser = new pb::MessageParser<ConsensusPrepareResponse>(() => new ConsensusPrepareResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConsensusPrepareResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Core.Proto.ConsensusReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPrepareResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPrepareResponse(ConsensusPrepareResponse other) : this() {
      signature_ = other.signature_ != null ? other.signature_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPrepareResponse Clone() {
      return new ConsensusPrepareResponse(this);
    }

    /// <summary>Field number for the "signature" field.</summary>
    public const int SignatureFieldNumber = 1;
    private global::Phorkus.Core.Proto.Signature signature_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.Signature Signature {
      get { return signature_; }
      set {
        signature_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConsensusPrepareResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConsensusPrepareResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Signature, other.Signature)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (signature_ != null) hash ^= Signature.GetHashCode();
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
      if (signature_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Signature);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (signature_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Signature);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConsensusPrepareResponse other) {
      if (other == null) {
        return;
      }
      if (other.signature_ != null) {
        if (signature_ == null) {
          signature_ = new global::Phorkus.Core.Proto.Signature();
        }
        Signature.MergeFrom(other.Signature);
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
            if (signature_ == null) {
              signature_ = new global::Phorkus.Core.Proto.Signature();
            }
            input.ReadMessage(signature_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class ConsensusPayloadCustomData : pb::IMessage<ConsensusPayloadCustomData> {
    private static readonly pb::MessageParser<ConsensusPayloadCustomData> _parser = new pb::MessageParser<ConsensusPayloadCustomData>(() => new ConsensusPayloadCustomData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConsensusPayloadCustomData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Phorkus.Core.Proto.ConsensusReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPayloadCustomData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPayloadCustomData(ConsensusPayloadCustomData other) : this() {
      type_ = other.type_;
      viewNumber_ = other.viewNumber_;
      switch (other.MessageCase) {
        case MessageOneofCase.ChangeView:
          ChangeView = other.ChangeView.Clone();
          break;
        case MessageOneofCase.PrepareRequest:
          PrepareRequest = other.PrepareRequest.Clone();
          break;
        case MessageOneofCase.PrepareResponse:
          PrepareResponse = other.PrepareResponse.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConsensusPayloadCustomData Clone() {
      return new ConsensusPayloadCustomData(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Phorkus.Core.Proto.ConsensusPayloadCustomData.Types.ConsensusMessageType type_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.ConsensusPayloadCustomData.Types.ConsensusMessageType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "view_number" field.</summary>
    public const int ViewNumberFieldNumber = 2;
    private uint viewNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint ViewNumber {
      get { return viewNumber_; }
      set {
        viewNumber_ = value;
      }
    }

    /// <summary>Field number for the "change_view" field.</summary>
    public const int ChangeViewFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.ConsensusChangeView ChangeView {
      get { return messageCase_ == MessageOneofCase.ChangeView ? (global::Phorkus.Core.Proto.ConsensusChangeView) message_ : null; }
      set {
        message_ = value;
        messageCase_ = value == null ? MessageOneofCase.None : MessageOneofCase.ChangeView;
      }
    }

    /// <summary>Field number for the "prepare_request" field.</summary>
    public const int PrepareRequestFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.ConsensusPrepareRequest PrepareRequest {
      get { return messageCase_ == MessageOneofCase.PrepareRequest ? (global::Phorkus.Core.Proto.ConsensusPrepareRequest) message_ : null; }
      set {
        message_ = value;
        messageCase_ = value == null ? MessageOneofCase.None : MessageOneofCase.PrepareRequest;
      }
    }

    /// <summary>Field number for the "prepare_response" field.</summary>
    public const int PrepareResponseFieldNumber = 5;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Phorkus.Core.Proto.ConsensusPrepareResponse PrepareResponse {
      get { return messageCase_ == MessageOneofCase.PrepareResponse ? (global::Phorkus.Core.Proto.ConsensusPrepareResponse) message_ : null; }
      set {
        message_ = value;
        messageCase_ = value == null ? MessageOneofCase.None : MessageOneofCase.PrepareResponse;
      }
    }

    private object message_;
    /// <summary>Enum of possible cases for the "message" oneof.</summary>
    public enum MessageOneofCase {
      None = 0,
      ChangeView = 3,
      PrepareRequest = 4,
      PrepareResponse = 5,
    }
    private MessageOneofCase messageCase_ = MessageOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageOneofCase MessageCase {
      get { return messageCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearMessage() {
      messageCase_ = MessageOneofCase.None;
      message_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConsensusPayloadCustomData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConsensusPayloadCustomData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (ViewNumber != other.ViewNumber) return false;
      if (!object.Equals(ChangeView, other.ChangeView)) return false;
      if (!object.Equals(PrepareRequest, other.PrepareRequest)) return false;
      if (!object.Equals(PrepareResponse, other.PrepareResponse)) return false;
      if (MessageCase != other.MessageCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != 0) hash ^= Type.GetHashCode();
      if (ViewNumber != 0) hash ^= ViewNumber.GetHashCode();
      if (messageCase_ == MessageOneofCase.ChangeView) hash ^= ChangeView.GetHashCode();
      if (messageCase_ == MessageOneofCase.PrepareRequest) hash ^= PrepareRequest.GetHashCode();
      if (messageCase_ == MessageOneofCase.PrepareResponse) hash ^= PrepareResponse.GetHashCode();
      hash ^= (int) messageCase_;
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
      if (ViewNumber != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(ViewNumber);
      }
      if (messageCase_ == MessageOneofCase.ChangeView) {
        output.WriteRawTag(26);
        output.WriteMessage(ChangeView);
      }
      if (messageCase_ == MessageOneofCase.PrepareRequest) {
        output.WriteRawTag(34);
        output.WriteMessage(PrepareRequest);
      }
      if (messageCase_ == MessageOneofCase.PrepareResponse) {
        output.WriteRawTag(42);
        output.WriteMessage(PrepareResponse);
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
      if (ViewNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(ViewNumber);
      }
      if (messageCase_ == MessageOneofCase.ChangeView) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ChangeView);
      }
      if (messageCase_ == MessageOneofCase.PrepareRequest) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PrepareRequest);
      }
      if (messageCase_ == MessageOneofCase.PrepareResponse) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PrepareResponse);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConsensusPayloadCustomData other) {
      if (other == null) {
        return;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.ViewNumber != 0) {
        ViewNumber = other.ViewNumber;
      }
      switch (other.MessageCase) {
        case MessageOneofCase.ChangeView:
          if (ChangeView == null) {
            ChangeView = new global::Phorkus.Core.Proto.ConsensusChangeView();
          }
          ChangeView.MergeFrom(other.ChangeView);
          break;
        case MessageOneofCase.PrepareRequest:
          if (PrepareRequest == null) {
            PrepareRequest = new global::Phorkus.Core.Proto.ConsensusPrepareRequest();
          }
          PrepareRequest.MergeFrom(other.PrepareRequest);
          break;
        case MessageOneofCase.PrepareResponse:
          if (PrepareResponse == null) {
            PrepareResponse = new global::Phorkus.Core.Proto.ConsensusPrepareResponse();
          }
          PrepareResponse.MergeFrom(other.PrepareResponse);
          break;
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
            type_ = (global::Phorkus.Core.Proto.ConsensusPayloadCustomData.Types.ConsensusMessageType) input.ReadEnum();
            break;
          }
          case 16: {
            ViewNumber = input.ReadUInt32();
            break;
          }
          case 26: {
            global::Phorkus.Core.Proto.ConsensusChangeView subBuilder = new global::Phorkus.Core.Proto.ConsensusChangeView();
            if (messageCase_ == MessageOneofCase.ChangeView) {
              subBuilder.MergeFrom(ChangeView);
            }
            input.ReadMessage(subBuilder);
            ChangeView = subBuilder;
            break;
          }
          case 34: {
            global::Phorkus.Core.Proto.ConsensusPrepareRequest subBuilder = new global::Phorkus.Core.Proto.ConsensusPrepareRequest();
            if (messageCase_ == MessageOneofCase.PrepareRequest) {
              subBuilder.MergeFrom(PrepareRequest);
            }
            input.ReadMessage(subBuilder);
            PrepareRequest = subBuilder;
            break;
          }
          case 42: {
            global::Phorkus.Core.Proto.ConsensusPrepareResponse subBuilder = new global::Phorkus.Core.Proto.ConsensusPrepareResponse();
            if (messageCase_ == MessageOneofCase.PrepareResponse) {
              subBuilder.MergeFrom(PrepareResponse);
            }
            input.ReadMessage(subBuilder);
            PrepareResponse = subBuilder;
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the ConsensusPayloadCustomData message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum ConsensusMessageType {
        [pbr::OriginalName("CONSENSUS_MESSAGE_TYPE_CHANGE_VIEW")] ChangeView = 0,
        [pbr::OriginalName("CONSENSUS_MESSAGE_TYPE_PREPARE_REQUEST")] PrepareRequest = 32,
        [pbr::OriginalName("CONSENSUS_MESSAGE_TYPE_PREPARE_RESPONSE")] PrepareResponse = 33,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
