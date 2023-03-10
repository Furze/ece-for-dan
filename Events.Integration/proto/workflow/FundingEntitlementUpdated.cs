// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: workflow/FundingEntitlementUpdated.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Events.Integration.Protobuf.Workflow {

  /// <summary>Holder for reflection information generated from workflow/FundingEntitlementUpdated.proto</summary>
  public static partial class FundingEntitlementUpdatedReflection {

    #region Descriptor
    /// <summary>File descriptor for workflow/FundingEntitlementUpdated.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static FundingEntitlementUpdatedReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cih3b3JrZmxvdy9GdW5kaW5nRW50aXRsZW1lbnRVcGRhdGVkLnByb3RvGhZw",
            "cm90b2J1Zi1uZXQvYmNsLnByb3RvGhFnb29nbGUvZGF0ZS5wcm90byKAAwoZ",
            "RnVuZGluZ0VudGl0bGVtZW50VXBkYXRlZBIVCgJJZBgBIAEoCzIJLmJjbC5H",
            "dWlkEiMKEEJ1c2luZXNzRW50aXR5SWQYAiABKAsyCS5iY2wuR3VpZBIUCgxT",
            "b3VyY2VTeXN0ZW0YAyABKAkSFwoPRW50aXRsZW1lbnRUeXBlGAQgASgJEjAK",
            "BlN0YXR1cxgFIAEoDjIgLkZ1bmRpbmdFbnRpdGxlbWVudFVwZGF0ZWRTdGF0",
            "dXMSFAoMUmVmZXJlbmNlVXJsGAYgASgJEh8KF1BheWVlT3JnYW5pc2F0aW9u",
            "TnVtYmVyGAggASgJEiUKHVBhcmVudFBheWVlT3JnYW5pc2F0aW9uTnVtYmVy",
            "GAkgASgJEhMKC0Rlc2NyaXB0aW9uGAogASgJEiMKCUxpbmVJdGVtcxgLIAMo",
            "CzIQLkludm9pY2VMaW5lSXRlbRIoCg1QYXltZW50TnpEYXRlGAwgASgLMhEu",
            "Z29vZ2xlLnR5cGUuRGF0ZUoECAcQCCKcAQoPSW52b2ljZUxpbmVJdGVtEiAK",
            "BkFtb3VudBgBIAEoCzIMLmJjbC5EZWNpbWFsQgIYARITCgtEZXNjcmlwdGlv",
            "bhgCIAEoCRIoChJBbW91bnRHc3RJbmNsdXNpdmUYAyABKAsyDC5iY2wuRGVj",
            "aW1hbBIoChJBbW91bnRHc3RFeGNsdXNpdmUYBCABKAsyDC5iY2wuRGVjaW1h",
            "bCqcAQofRnVuZGluZ0VudGl0bGVtZW50VXBkYXRlZFN0YXR1cxIWChJFbnRp",
            "dGxlbWVudENyZWF0ZWQQABIXChNFbnRpdGxlbWVudEFwcHJvdmVkEAESFgoS",
            "RW50aXRsZW1lbnRVcGRhdGVkEAISFgoSRW50aXRsZW1lbnREZWxldGVkEAMS",
            "GAoURW50aXRsZW1lbnRDYW5jZWxsZWQQBEInqgIkRXZlbnRzLkludGVncmF0",
            "aW9uLlByb3RvYnVmLldvcmtmbG93YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::ProtoBuf.Bcl.BclReflection.Descriptor, global::Google.Type.DateReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdated), global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdated.Parser, new[]{ "Id", "BusinessEntityId", "SourceSystem", "EntitlementType", "Status", "ReferenceUrl", "PayeeOrganisationNumber", "ParentPayeeOrganisationNumber", "Description", "LineItems", "PaymentNzDate" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.InvoiceLineItem), global::Events.Integration.Protobuf.Workflow.InvoiceLineItem.Parser, new[]{ "Amount", "Description", "AmountGstInclusive", "AmountGstExclusive" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum FundingEntitlementUpdatedStatus {
    [pbr::OriginalName("EntitlementCreated")] EntitlementCreated = 0,
    [pbr::OriginalName("EntitlementApproved")] EntitlementApproved = 1,
    [pbr::OriginalName("EntitlementUpdated")] EntitlementUpdated = 2,
    [pbr::OriginalName("EntitlementDeleted")] EntitlementDeleted = 3,
    [pbr::OriginalName("EntitlementCancelled")] EntitlementCancelled = 4,
  }

  #endregion

  #region Messages
  public sealed partial class FundingEntitlementUpdated : pb::IMessage<FundingEntitlementUpdated>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<FundingEntitlementUpdated> _parser = new pb::MessageParser<FundingEntitlementUpdated>(() => new FundingEntitlementUpdated());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<FundingEntitlementUpdated> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FundingEntitlementUpdated() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FundingEntitlementUpdated(FundingEntitlementUpdated other) : this() {
      id_ = other.id_ != null ? other.id_.Clone() : null;
      businessEntityId_ = other.businessEntityId_ != null ? other.businessEntityId_.Clone() : null;
      sourceSystem_ = other.sourceSystem_;
      entitlementType_ = other.entitlementType_;
      status_ = other.status_;
      referenceUrl_ = other.referenceUrl_;
      payeeOrganisationNumber_ = other.payeeOrganisationNumber_;
      parentPayeeOrganisationNumber_ = other.parentPayeeOrganisationNumber_;
      description_ = other.description_;
      lineItems_ = other.lineItems_.Clone();
      paymentNzDate_ = other.paymentNzDate_ != null ? other.paymentNzDate_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FundingEntitlementUpdated Clone() {
      return new FundingEntitlementUpdated(this);
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 1;
    private global::ProtoBuf.Bcl.Guid id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Guid Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "BusinessEntityId" field.</summary>
    public const int BusinessEntityIdFieldNumber = 2;
    private global::ProtoBuf.Bcl.Guid businessEntityId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Guid BusinessEntityId {
      get { return businessEntityId_; }
      set {
        businessEntityId_ = value;
      }
    }

    /// <summary>Field number for the "SourceSystem" field.</summary>
    public const int SourceSystemFieldNumber = 3;
    private string sourceSystem_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string SourceSystem {
      get { return sourceSystem_; }
      set {
        sourceSystem_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "EntitlementType" field.</summary>
    public const int EntitlementTypeFieldNumber = 4;
    private string entitlementType_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string EntitlementType {
      get { return entitlementType_; }
      set {
        entitlementType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Status" field.</summary>
    public const int StatusFieldNumber = 5;
    private global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus status_ = global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus.EntitlementCreated;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    /// <summary>Field number for the "ReferenceUrl" field.</summary>
    public const int ReferenceUrlFieldNumber = 6;
    private string referenceUrl_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ReferenceUrl {
      get { return referenceUrl_; }
      set {
        referenceUrl_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "PayeeOrganisationNumber" field.</summary>
    public const int PayeeOrganisationNumberFieldNumber = 8;
    private string payeeOrganisationNumber_ = "";
    /// <summary>
    ///google.protobuf.Timestamp PaymentDate = 7;
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PayeeOrganisationNumber {
      get { return payeeOrganisationNumber_; }
      set {
        payeeOrganisationNumber_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ParentPayeeOrganisationNumber" field.</summary>
    public const int ParentPayeeOrganisationNumberFieldNumber = 9;
    private string parentPayeeOrganisationNumber_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ParentPayeeOrganisationNumber {
      get { return parentPayeeOrganisationNumber_; }
      set {
        parentPayeeOrganisationNumber_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Description" field.</summary>
    public const int DescriptionFieldNumber = 10;
    private string description_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Description {
      get { return description_; }
      set {
        description_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "LineItems" field.</summary>
    public const int LineItemsFieldNumber = 11;
    private static readonly pb::FieldCodec<global::Events.Integration.Protobuf.Workflow.InvoiceLineItem> _repeated_lineItems_codec
        = pb::FieldCodec.ForMessage(90, global::Events.Integration.Protobuf.Workflow.InvoiceLineItem.Parser);
    private readonly pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.InvoiceLineItem> lineItems_ = new pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.InvoiceLineItem>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.InvoiceLineItem> LineItems {
      get { return lineItems_; }
    }

    /// <summary>Field number for the "PaymentNzDate" field.</summary>
    public const int PaymentNzDateFieldNumber = 12;
    private global::Google.Type.Date paymentNzDate_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Type.Date PaymentNzDate {
      get { return paymentNzDate_; }
      set {
        paymentNzDate_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as FundingEntitlementUpdated);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(FundingEntitlementUpdated other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Id, other.Id)) return false;
      if (!object.Equals(BusinessEntityId, other.BusinessEntityId)) return false;
      if (SourceSystem != other.SourceSystem) return false;
      if (EntitlementType != other.EntitlementType) return false;
      if (Status != other.Status) return false;
      if (ReferenceUrl != other.ReferenceUrl) return false;
      if (PayeeOrganisationNumber != other.PayeeOrganisationNumber) return false;
      if (ParentPayeeOrganisationNumber != other.ParentPayeeOrganisationNumber) return false;
      if (Description != other.Description) return false;
      if(!lineItems_.Equals(other.lineItems_)) return false;
      if (!object.Equals(PaymentNzDate, other.PaymentNzDate)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (id_ != null) hash ^= Id.GetHashCode();
      if (businessEntityId_ != null) hash ^= BusinessEntityId.GetHashCode();
      if (SourceSystem.Length != 0) hash ^= SourceSystem.GetHashCode();
      if (EntitlementType.Length != 0) hash ^= EntitlementType.GetHashCode();
      if (Status != global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus.EntitlementCreated) hash ^= Status.GetHashCode();
      if (ReferenceUrl.Length != 0) hash ^= ReferenceUrl.GetHashCode();
      if (PayeeOrganisationNumber.Length != 0) hash ^= PayeeOrganisationNumber.GetHashCode();
      if (ParentPayeeOrganisationNumber.Length != 0) hash ^= ParentPayeeOrganisationNumber.GetHashCode();
      if (Description.Length != 0) hash ^= Description.GetHashCode();
      hash ^= lineItems_.GetHashCode();
      if (paymentNzDate_ != null) hash ^= PaymentNzDate.GetHashCode();
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
      if (id_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Id);
      }
      if (businessEntityId_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(BusinessEntityId);
      }
      if (SourceSystem.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(SourceSystem);
      }
      if (EntitlementType.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(EntitlementType);
      }
      if (Status != global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus.EntitlementCreated) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (ReferenceUrl.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(ReferenceUrl);
      }
      if (PayeeOrganisationNumber.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(PayeeOrganisationNumber);
      }
      if (ParentPayeeOrganisationNumber.Length != 0) {
        output.WriteRawTag(74);
        output.WriteString(ParentPayeeOrganisationNumber);
      }
      if (Description.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Description);
      }
      lineItems_.WriteTo(output, _repeated_lineItems_codec);
      if (paymentNzDate_ != null) {
        output.WriteRawTag(98);
        output.WriteMessage(PaymentNzDate);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (id_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Id);
      }
      if (businessEntityId_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(BusinessEntityId);
      }
      if (SourceSystem.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(SourceSystem);
      }
      if (EntitlementType.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(EntitlementType);
      }
      if (Status != global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus.EntitlementCreated) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (ReferenceUrl.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(ReferenceUrl);
      }
      if (PayeeOrganisationNumber.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(PayeeOrganisationNumber);
      }
      if (ParentPayeeOrganisationNumber.Length != 0) {
        output.WriteRawTag(74);
        output.WriteString(ParentPayeeOrganisationNumber);
      }
      if (Description.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Description);
      }
      lineItems_.WriteTo(ref output, _repeated_lineItems_codec);
      if (paymentNzDate_ != null) {
        output.WriteRawTag(98);
        output.WriteMessage(PaymentNzDate);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (id_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Id);
      }
      if (businessEntityId_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(BusinessEntityId);
      }
      if (SourceSystem.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SourceSystem);
      }
      if (EntitlementType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(EntitlementType);
      }
      if (Status != global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus.EntitlementCreated) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (ReferenceUrl.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ReferenceUrl);
      }
      if (PayeeOrganisationNumber.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PayeeOrganisationNumber);
      }
      if (ParentPayeeOrganisationNumber.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ParentPayeeOrganisationNumber);
      }
      if (Description.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Description);
      }
      size += lineItems_.CalculateSize(_repeated_lineItems_codec);
      if (paymentNzDate_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PaymentNzDate);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(FundingEntitlementUpdated other) {
      if (other == null) {
        return;
      }
      if (other.id_ != null) {
        if (id_ == null) {
          Id = new global::ProtoBuf.Bcl.Guid();
        }
        Id.MergeFrom(other.Id);
      }
      if (other.businessEntityId_ != null) {
        if (businessEntityId_ == null) {
          BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
        }
        BusinessEntityId.MergeFrom(other.BusinessEntityId);
      }
      if (other.SourceSystem.Length != 0) {
        SourceSystem = other.SourceSystem;
      }
      if (other.EntitlementType.Length != 0) {
        EntitlementType = other.EntitlementType;
      }
      if (other.Status != global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus.EntitlementCreated) {
        Status = other.Status;
      }
      if (other.ReferenceUrl.Length != 0) {
        ReferenceUrl = other.ReferenceUrl;
      }
      if (other.PayeeOrganisationNumber.Length != 0) {
        PayeeOrganisationNumber = other.PayeeOrganisationNumber;
      }
      if (other.ParentPayeeOrganisationNumber.Length != 0) {
        ParentPayeeOrganisationNumber = other.ParentPayeeOrganisationNumber;
      }
      if (other.Description.Length != 0) {
        Description = other.Description;
      }
      lineItems_.Add(other.lineItems_);
      if (other.paymentNzDate_ != null) {
        if (paymentNzDate_ == null) {
          PaymentNzDate = new global::Google.Type.Date();
        }
        PaymentNzDate.MergeFrom(other.PaymentNzDate);
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
            if (id_ == null) {
              Id = new global::ProtoBuf.Bcl.Guid();
            }
            input.ReadMessage(Id);
            break;
          }
          case 18: {
            if (businessEntityId_ == null) {
              BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
            }
            input.ReadMessage(BusinessEntityId);
            break;
          }
          case 26: {
            SourceSystem = input.ReadString();
            break;
          }
          case 34: {
            EntitlementType = input.ReadString();
            break;
          }
          case 40: {
            Status = (global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus) input.ReadEnum();
            break;
          }
          case 50: {
            ReferenceUrl = input.ReadString();
            break;
          }
          case 66: {
            PayeeOrganisationNumber = input.ReadString();
            break;
          }
          case 74: {
            ParentPayeeOrganisationNumber = input.ReadString();
            break;
          }
          case 82: {
            Description = input.ReadString();
            break;
          }
          case 90: {
            lineItems_.AddEntriesFrom(input, _repeated_lineItems_codec);
            break;
          }
          case 98: {
            if (paymentNzDate_ == null) {
              PaymentNzDate = new global::Google.Type.Date();
            }
            input.ReadMessage(PaymentNzDate);
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
            if (id_ == null) {
              Id = new global::ProtoBuf.Bcl.Guid();
            }
            input.ReadMessage(Id);
            break;
          }
          case 18: {
            if (businessEntityId_ == null) {
              BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
            }
            input.ReadMessage(BusinessEntityId);
            break;
          }
          case 26: {
            SourceSystem = input.ReadString();
            break;
          }
          case 34: {
            EntitlementType = input.ReadString();
            break;
          }
          case 40: {
            Status = (global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedStatus) input.ReadEnum();
            break;
          }
          case 50: {
            ReferenceUrl = input.ReadString();
            break;
          }
          case 66: {
            PayeeOrganisationNumber = input.ReadString();
            break;
          }
          case 74: {
            ParentPayeeOrganisationNumber = input.ReadString();
            break;
          }
          case 82: {
            Description = input.ReadString();
            break;
          }
          case 90: {
            lineItems_.AddEntriesFrom(ref input, _repeated_lineItems_codec);
            break;
          }
          case 98: {
            if (paymentNzDate_ == null) {
              PaymentNzDate = new global::Google.Type.Date();
            }
            input.ReadMessage(PaymentNzDate);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class InvoiceLineItem : pb::IMessage<InvoiceLineItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<InvoiceLineItem> _parser = new pb::MessageParser<InvoiceLineItem>(() => new InvoiceLineItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<InvoiceLineItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Workflow.FundingEntitlementUpdatedReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InvoiceLineItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InvoiceLineItem(InvoiceLineItem other) : this() {
      amount_ = other.amount_ != null ? other.amount_.Clone() : null;
      description_ = other.description_;
      amountGstInclusive_ = other.amountGstInclusive_ != null ? other.amountGstInclusive_.Clone() : null;
      amountGstExclusive_ = other.amountGstExclusive_ != null ? other.amountGstExclusive_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InvoiceLineItem Clone() {
      return new InvoiceLineItem(this);
    }

    /// <summary>Field number for the "Amount" field.</summary>
    public const int AmountFieldNumber = 1;
    private global::ProtoBuf.Bcl.Decimal amount_;
    [global::System.ObsoleteAttribute]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Decimal Amount {
      get { return amount_; }
      set {
        amount_ = value;
      }
    }

    /// <summary>Field number for the "Description" field.</summary>
    public const int DescriptionFieldNumber = 2;
    private string description_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Description {
      get { return description_; }
      set {
        description_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "AmountGstInclusive" field.</summary>
    public const int AmountGstInclusiveFieldNumber = 3;
    private global::ProtoBuf.Bcl.Decimal amountGstInclusive_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Decimal AmountGstInclusive {
      get { return amountGstInclusive_; }
      set {
        amountGstInclusive_ = value;
      }
    }

    /// <summary>Field number for the "AmountGstExclusive" field.</summary>
    public const int AmountGstExclusiveFieldNumber = 4;
    private global::ProtoBuf.Bcl.Decimal amountGstExclusive_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoBuf.Bcl.Decimal AmountGstExclusive {
      get { return amountGstExclusive_; }
      set {
        amountGstExclusive_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as InvoiceLineItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(InvoiceLineItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Amount, other.Amount)) return false;
      if (Description != other.Description) return false;
      if (!object.Equals(AmountGstInclusive, other.AmountGstInclusive)) return false;
      if (!object.Equals(AmountGstExclusive, other.AmountGstExclusive)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (amount_ != null) hash ^= Amount.GetHashCode();
      if (Description.Length != 0) hash ^= Description.GetHashCode();
      if (amountGstInclusive_ != null) hash ^= AmountGstInclusive.GetHashCode();
      if (amountGstExclusive_ != null) hash ^= AmountGstExclusive.GetHashCode();
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
      if (amount_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Amount);
      }
      if (Description.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Description);
      }
      if (amountGstInclusive_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(AmountGstInclusive);
      }
      if (amountGstExclusive_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(AmountGstExclusive);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (amount_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Amount);
      }
      if (Description.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Description);
      }
      if (amountGstInclusive_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(AmountGstInclusive);
      }
      if (amountGstExclusive_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(AmountGstExclusive);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (amount_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Amount);
      }
      if (Description.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Description);
      }
      if (amountGstInclusive_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AmountGstInclusive);
      }
      if (amountGstExclusive_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AmountGstExclusive);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(InvoiceLineItem other) {
      if (other == null) {
        return;
      }
      if (other.amount_ != null) {
        if (amount_ == null) {
          Amount = new global::ProtoBuf.Bcl.Decimal();
        }
        Amount.MergeFrom(other.Amount);
      }
      if (other.Description.Length != 0) {
        Description = other.Description;
      }
      if (other.amountGstInclusive_ != null) {
        if (amountGstInclusive_ == null) {
          AmountGstInclusive = new global::ProtoBuf.Bcl.Decimal();
        }
        AmountGstInclusive.MergeFrom(other.AmountGstInclusive);
      }
      if (other.amountGstExclusive_ != null) {
        if (amountGstExclusive_ == null) {
          AmountGstExclusive = new global::ProtoBuf.Bcl.Decimal();
        }
        AmountGstExclusive.MergeFrom(other.AmountGstExclusive);
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
            if (amount_ == null) {
              Amount = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(Amount);
            break;
          }
          case 18: {
            Description = input.ReadString();
            break;
          }
          case 26: {
            if (amountGstInclusive_ == null) {
              AmountGstInclusive = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(AmountGstInclusive);
            break;
          }
          case 34: {
            if (amountGstExclusive_ == null) {
              AmountGstExclusive = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(AmountGstExclusive);
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
            if (amount_ == null) {
              Amount = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(Amount);
            break;
          }
          case 18: {
            Description = input.ReadString();
            break;
          }
          case 26: {
            if (amountGstInclusive_ == null) {
              AmountGstInclusive = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(AmountGstInclusive);
            break;
          }
          case 34: {
            if (amountGstExclusive_ == null) {
              AmountGstExclusive = new global::ProtoBuf.Bcl.Decimal();
            }
            input.ReadMessage(AmountGstExclusive);
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
