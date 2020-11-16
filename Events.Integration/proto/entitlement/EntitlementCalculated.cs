// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: entitlement/EntitlementCalculated.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Events.Integration.Protobuf.Entitlement {

  /// <summary>Holder for reflection information generated from entitlement/EntitlementCalculated.proto</summary>
  public static partial class EntitlementCalculatedReflection {

    #region Descriptor
    /// <summary>File descriptor for entitlement/EntitlementCalculated.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static EntitlementCalculatedReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CidlbnRpdGxlbWVudC9FbnRpdGxlbWVudENhbGN1bGF0ZWQucHJvdG8aFnBy",
            "b3RvYnVmLW5ldC9iY2wucHJvdG8aH3NoYXJlZC9GdW5kaW5nUGVyaW9kTW9u",
            "dGgucHJvdG8ihAIKFUVudGl0bGVtZW50Q2FsY3VsYXRlZBIjChBCdXNpbmVz",
            "c0VudGl0eUlkGAEgASgLMgkuYmNsLkd1aWQSFgoOT3JnYW5pc2F0aW9uSWQY",
            "AiABKAUSEQoJUmVxdWVzdElkGAMgASgJEhoKEkJ1c2luZXNzRW50aXR5VHlw",
            "ZRgEIAEoCRIvChJGdW5kaW5nUGVyaW9kTW9udGgYBSABKA4yEy5GdW5kaW5n",
            "UGVyaW9kTW9udGgSEwoLRnVuZGluZ1llYXIYBiABKAUSFgoOUmV2aXNpb25O",
            "dW1iZXIYByABKAUSIQoLVG90YWxXYXNoVXAYCCABKAsyDC5iY2wuRGVjaW1h",
            "bEIqqgInRXZlbnRzLkludGVncmF0aW9uLlByb3RvYnVmLkVudGl0bGVtZW50",
            "YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::ProtoBuf.Bcl.BclReflection.Descriptor, global::Events.Integration.Protobuf.Shared.FundingPeriodMonthReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Entitlement.EntitlementCalculated), global::Events.Integration.Protobuf.Entitlement.EntitlementCalculated.Parser, new[]{ "BusinessEntityId", "OrganisationId", "RequestId", "BusinessEntityType", "FundingPeriodMonth", "FundingYear", "RevisionNumber", "TotalWashUp" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///Entitlement.EntitlementCalculated
  /// </summary>
  public sealed partial class EntitlementCalculated : pb::IMessage<EntitlementCalculated>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<EntitlementCalculated> _parser = new pb::MessageParser<EntitlementCalculated>(() => new EntitlementCalculated());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<EntitlementCalculated> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Entitlement.EntitlementCalculatedReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EntitlementCalculated() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EntitlementCalculated(EntitlementCalculated other) : this() {
      businessEntityId_ = other.businessEntityId_ != null ? other.businessEntityId_.Clone() : null;
      organisationId_ = other.organisationId_;
      requestId_ = other.requestId_;
      businessEntityType_ = other.businessEntityType_;
      fundingPeriodMonth_ = other.fundingPeriodMonth_;
      fundingYear_ = other.fundingYear_;
      revisionNumber_ = other.revisionNumber_;
      totalWashUp_ = other.totalWashUp_ != null ? other.totalWashUp_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EntitlementCalculated Clone() {
      return new EntitlementCalculated(this);
    }

    /// <summary>Field number for the "BusinessEntityId" field.</summary>
    public const int BusinessEntityIdFieldNumber = 1;
    private global::ProtoBuf.Bcl.Guid businessEntityId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Guid BusinessEntityId {
      get { return businessEntityId_; }
      set {
        businessEntityId_ = value;
      }
    }

    /// <summary>Field number for the "OrganisationId" field.</summary>
    public const int OrganisationIdFieldNumber = 2;
    private int organisationId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int OrganisationId {
      get { return organisationId_; }
      set {
        organisationId_ = value;
      }
    }

    /// <summary>Field number for the "RequestId" field.</summary>
    public const int RequestIdFieldNumber = 3;
    private string requestId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string RequestId {
      get { return requestId_; }
      set {
        requestId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "BusinessEntityType" field.</summary>
    public const int BusinessEntityTypeFieldNumber = 4;
    private string businessEntityType_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string BusinessEntityType {
      get { return businessEntityType_; }
      set {
        businessEntityType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "FundingPeriodMonth" field.</summary>
    public const int FundingPeriodMonthFieldNumber = 5;
    private global::Events.Integration.Protobuf.Shared.FundingPeriodMonth fundingPeriodMonth_ = global::Events.Integration.Protobuf.Shared.FundingPeriodMonth.Unspecified;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Events.Integration.Protobuf.Shared.FundingPeriodMonth FundingPeriodMonth {
      get { return fundingPeriodMonth_; }
      set {
        fundingPeriodMonth_ = value;
      }
    }

    /// <summary>Field number for the "FundingYear" field.</summary>
    public const int FundingYearFieldNumber = 6;
    private int fundingYear_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FundingYear {
      get { return fundingYear_; }
      set {
        fundingYear_ = value;
      }
    }

    /// <summary>Field number for the "RevisionNumber" field.</summary>
    public const int RevisionNumberFieldNumber = 7;
    private int revisionNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RevisionNumber {
      get { return revisionNumber_; }
      set {
        revisionNumber_ = value;
      }
    }

    /// <summary>Field number for the "TotalWashUp" field.</summary>
    public const int TotalWashUpFieldNumber = 8;
    private global::ProtoBuf.Bcl.Decimal totalWashUp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Decimal TotalWashUp {
      get { return totalWashUp_; }
      set {
        totalWashUp_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as EntitlementCalculated);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(EntitlementCalculated other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(BusinessEntityId, other.BusinessEntityId)) return false;
      if (OrganisationId != other.OrganisationId) return false;
      if (RequestId != other.RequestId) return false;
      if (BusinessEntityType != other.BusinessEntityType) return false;
      if (FundingPeriodMonth != other.FundingPeriodMonth) return false;
      if (FundingYear != other.FundingYear) return false;
      if (RevisionNumber != other.RevisionNumber) return false;
      if (!object.Equals(TotalWashUp, other.TotalWashUp)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (businessEntityId_ != null) hash ^= BusinessEntityId.GetHashCode();
      if (OrganisationId != 0) hash ^= OrganisationId.GetHashCode();
      if (RequestId.Length != 0) hash ^= RequestId.GetHashCode();
      if (BusinessEntityType.Length != 0) hash ^= BusinessEntityType.GetHashCode();
      if (FundingPeriodMonth != global::Events.Integration.Protobuf.Shared.FundingPeriodMonth.Unspecified) hash ^= FundingPeriodMonth.GetHashCode();
      if (FundingYear != 0) hash ^= FundingYear.GetHashCode();
      if (RevisionNumber != 0) hash ^= RevisionNumber.GetHashCode();
      if (totalWashUp_ != null) hash ^= TotalWashUp.GetHashCode();
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
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (businessEntityId_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(BusinessEntityId);
      }
      if (OrganisationId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(OrganisationId);
      }
      if (RequestId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(RequestId);
      }
      if (BusinessEntityType.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(BusinessEntityType);
      }
      if (FundingPeriodMonth != global::Events.Integration.Protobuf.Shared.FundingPeriodMonth.Unspecified) {
        output.WriteRawTag(40);
        output.WriteEnum((int) FundingPeriodMonth);
      }
      if (FundingYear != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(FundingYear);
      }
      if (RevisionNumber != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(RevisionNumber);
      }
      if (totalWashUp_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(TotalWashUp);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (businessEntityId_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(BusinessEntityId);
      }
      if (OrganisationId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(OrganisationId);
      }
      if (RequestId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(RequestId);
      }
      if (BusinessEntityType.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(BusinessEntityType);
      }
      if (FundingPeriodMonth != global::Events.Integration.Protobuf.Shared.FundingPeriodMonth.Unspecified) {
        output.WriteRawTag(40);
        output.WriteEnum((int) FundingPeriodMonth);
      }
      if (FundingYear != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(FundingYear);
      }
      if (RevisionNumber != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(RevisionNumber);
      }
      if (totalWashUp_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(TotalWashUp);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (businessEntityId_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(BusinessEntityId);
      }
      if (OrganisationId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(OrganisationId);
      }
      if (RequestId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(RequestId);
      }
      if (BusinessEntityType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BusinessEntityType);
      }
      if (FundingPeriodMonth != global::Events.Integration.Protobuf.Shared.FundingPeriodMonth.Unspecified) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) FundingPeriodMonth);
      }
      if (FundingYear != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FundingYear);
      }
      if (RevisionNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RevisionNumber);
      }
      if (totalWashUp_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(TotalWashUp);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(EntitlementCalculated other) {
      if (other == null) {
        return;
      }
      if (other.businessEntityId_ != null) {
        if (businessEntityId_ == null) {
          BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
        }
        BusinessEntityId.MergeFrom(other.BusinessEntityId);
      }
      if (other.OrganisationId != 0) {
        OrganisationId = other.OrganisationId;
      }
      if (other.RequestId.Length != 0) {
        RequestId = other.RequestId;
      }
      if (other.BusinessEntityType.Length != 0) {
        BusinessEntityType = other.BusinessEntityType;
      }
      if (other.FundingPeriodMonth != global::Events.Integration.Protobuf.Shared.FundingPeriodMonth.Unspecified) {
        FundingPeriodMonth = other.FundingPeriodMonth;
      }
      if (other.FundingYear != 0) {
        FundingYear = other.FundingYear;
      }
      if (other.RevisionNumber != 0) {
        RevisionNumber = other.RevisionNumber;
      }
      if (other.totalWashUp_ != null) {
        if (totalWashUp_ == null) {
          TotalWashUp = new global::ProtoBuf.Bcl.Decimal();
        }
        TotalWashUp.MergeFrom(other.TotalWashUp);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (businessEntityId_ == null) {
              BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
            }
            input.ReadMessage(BusinessEntityId);
            break;
          }
          case 16: {
            OrganisationId = input.ReadInt32();
            break;
          }
          case 26: {
            RequestId = input.ReadString();
            break;
          }
          case 34: {
            BusinessEntityType = input.ReadString();
            break;
          }
          case 40: {
            FundingPeriodMonth = (global::Events.Integration.Protobuf.Shared.FundingPeriodMonth) input.ReadEnum();
            break;
          }
          case 48: {
            FundingYear = input.ReadInt32();
            break;
          }
          case 56: {
            RevisionNumber = input.ReadInt32();
            break;
          }
          case 66: {
            if (totalWashUp_ == null) {
              TotalWashUp = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(TotalWashUp);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (businessEntityId_ == null) {
              BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
            }
            input.ReadMessage(BusinessEntityId);
            break;
          }
          case 16: {
            OrganisationId = input.ReadInt32();
            break;
          }
          case 26: {
            RequestId = input.ReadString();
            break;
          }
          case 34: {
            BusinessEntityType = input.ReadString();
            break;
          }
          case 40: {
            FundingPeriodMonth = (global::Events.Integration.Protobuf.Shared.FundingPeriodMonth) input.ReadEnum();
            break;
          }
          case 48: {
            FundingYear = input.ReadInt32();
            break;
          }
          case 56: {
            RevisionNumber = input.ReadInt32();
            break;
          }
          case 66: {
            if (totalWashUp_ == null) {
              TotalWashUp = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(TotalWashUp);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
