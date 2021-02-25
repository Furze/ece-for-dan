// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: workflow/common/WorkflowTransition.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Events.Integration.Protobuf.Workflow.Common {

  /// <summary>Holder for reflection information generated from workflow/common/WorkflowTransition.proto</summary>
  public static partial class WorkflowTransitionReflection {

    #region Descriptor
    /// <summary>File descriptor for workflow/common/WorkflowTransition.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static WorkflowTransitionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cih3b3JrZmxvdy9jb21tb24vV29ya2Zsb3dUcmFuc2l0aW9uLnByb3RvEg93",
            "b3JrZmxvdy5jb21tb24itwEKEldvcmtmbG93VHJhbnNpdGlvbhINCgVWYWx1",
            "ZRgBIAEoBRITCgtEaXNwbGF5TmFtZRgCIAEoCRI1Cg1Xb3JrZmxvd1N0YXRl",
            "GAMgASgOMh4ud29ya2Zsb3cuY29tbW9uLldvcmtmbG93U3RhdGUSEgoKSXNU",
            "ZXJtaW5hbBgEIAEoCBIyCgdSZWFzb25zGAUgAygLMiEud29ya2Zsb3cuY29t",
            "bW9uLlRyYW5zaXRpb25SZWFzb24iLgoQVHJhbnNpdGlvblJlYXNvbhIKCgJJ",
            "ZBgBIAEoBRIOCgZSZWFzb24YAiABKAkq4gEKDVdvcmtmbG93U3RhdGUSEgoO",
            "UmVhZHlGb3JSZXZpZXcQABINCglTdWJtaXR0ZWQQARIMCghBcHByb3ZlZBAC",
            "EgwKCFJldHVybmVkEAQSFgoSUmVjb21tZW5kVG9FbmRvcnNlEAUSFgoSUmVj",
            "b21tZW5kVG9EZWNsaW5lEAoSEgoOUmVhZHlUb0VuZG9yc2UQCxIKCgZPbkhv",
            "bGQQBhIQCgxJblBlZXJSZXZpZXcQBxIMCghEZWNsaW5lZBAIEhQKEFJlYWR5",
            "Rm9yQXBwcm92YWwQCRIMCghFbmRvcnNlZBAMQi6qAitFdmVudHMuSW50ZWdy",
            "YXRpb24uUHJvdG9idWYuV29ya2Zsb3cuQ29tbW9uYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Events.Integration.Protobuf.Workflow.Common.WorkflowState), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition), global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition.Parser, new[]{ "Value", "DisplayName", "WorkflowState", "IsTerminal", "Reasons" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.Common.TransitionReason), global::Events.Integration.Protobuf.Workflow.Common.TransitionReason.Parser, new[]{ "Id", "Reason" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum WorkflowState {
    [pbr::OriginalName("ReadyForReview")] ReadyForReview = 0,
    [pbr::OriginalName("Submitted")] Submitted = 1,
    [pbr::OriginalName("Approved")] Approved = 2,
    [pbr::OriginalName("Returned")] Returned = 4,
    [pbr::OriginalName("RecommendToEndorse")] RecommendToEndorse = 5,
    [pbr::OriginalName("RecommendToDecline")] RecommendToDecline = 10,
    [pbr::OriginalName("ReadyToEndorse")] ReadyToEndorse = 11,
    [pbr::OriginalName("OnHold")] OnHold = 6,
    [pbr::OriginalName("InPeerReview")] InPeerReview = 7,
    [pbr::OriginalName("Declined")] Declined = 8,
    [pbr::OriginalName("ReadyForApproval")] ReadyForApproval = 9,
    [pbr::OriginalName("Endorsed")] Endorsed = 12,
  }

  #endregion

  #region Messages
  public sealed partial class WorkflowTransition : pb::IMessage<WorkflowTransition>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<WorkflowTransition> _parser = new pb::MessageParser<WorkflowTransition>(() => new WorkflowTransition());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<WorkflowTransition> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransitionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WorkflowTransition() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WorkflowTransition(WorkflowTransition other) : this() {
      value_ = other.value_;
      displayName_ = other.displayName_;
      workflowState_ = other.workflowState_;
      isTerminal_ = other.isTerminal_;
      reasons_ = other.reasons_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WorkflowTransition Clone() {
      return new WorkflowTransition(this);
    }

    /// <summary>Field number for the "Value" field.</summary>
    public const int ValueFieldNumber = 1;
    private int value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    /// <summary>Field number for the "DisplayName" field.</summary>
    public const int DisplayNameFieldNumber = 2;
    private string displayName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string DisplayName {
      get { return displayName_; }
      set {
        displayName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "WorkflowState" field.</summary>
    public const int WorkflowStateFieldNumber = 3;
    private global::Events.Integration.Protobuf.Workflow.Common.WorkflowState workflowState_ = global::Events.Integration.Protobuf.Workflow.Common.WorkflowState.ReadyForReview;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Events.Integration.Protobuf.Workflow.Common.WorkflowState WorkflowState {
      get { return workflowState_; }
      set {
        workflowState_ = value;
      }
    }

    /// <summary>Field number for the "IsTerminal" field.</summary>
    public const int IsTerminalFieldNumber = 4;
    private bool isTerminal_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsTerminal {
      get { return isTerminal_; }
      set {
        isTerminal_ = value;
      }
    }

    /// <summary>Field number for the "Reasons" field.</summary>
    public const int ReasonsFieldNumber = 5;
    private static readonly pb::FieldCodec<global::Events.Integration.Protobuf.Workflow.Common.TransitionReason> _repeated_reasons_codec
        = pb::FieldCodec.ForMessage(42, global::Events.Integration.Protobuf.Workflow.Common.TransitionReason.Parser);
    private readonly pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.Common.TransitionReason> reasons_ = new pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.Common.TransitionReason>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.Common.TransitionReason> Reasons {
      get { return reasons_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as WorkflowTransition);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(WorkflowTransition other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      if (DisplayName != other.DisplayName) return false;
      if (WorkflowState != other.WorkflowState) return false;
      if (IsTerminal != other.IsTerminal) return false;
      if(!reasons_.Equals(other.reasons_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value != 0) hash ^= Value.GetHashCode();
      if (DisplayName.Length != 0) hash ^= DisplayName.GetHashCode();
      if (WorkflowState != global::Events.Integration.Protobuf.Workflow.Common.WorkflowState.ReadyForReview) hash ^= WorkflowState.GetHashCode();
      if (IsTerminal != false) hash ^= IsTerminal.GetHashCode();
      hash ^= reasons_.GetHashCode();
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
      if (Value != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Value);
      }
      if (DisplayName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DisplayName);
      }
      if (WorkflowState != global::Events.Integration.Protobuf.Workflow.Common.WorkflowState.ReadyForReview) {
        output.WriteRawTag(24);
        output.WriteEnum((int) WorkflowState);
      }
      if (IsTerminal != false) {
        output.WriteRawTag(32);
        output.WriteBool(IsTerminal);
      }
      reasons_.WriteTo(output, _repeated_reasons_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Value != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Value);
      }
      if (DisplayName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DisplayName);
      }
      if (WorkflowState != global::Events.Integration.Protobuf.Workflow.Common.WorkflowState.ReadyForReview) {
        output.WriteRawTag(24);
        output.WriteEnum((int) WorkflowState);
      }
      if (IsTerminal != false) {
        output.WriteRawTag(32);
        output.WriteBool(IsTerminal);
      }
      reasons_.WriteTo(ref output, _repeated_reasons_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Value);
      }
      if (DisplayName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(DisplayName);
      }
      if (WorkflowState != global::Events.Integration.Protobuf.Workflow.Common.WorkflowState.ReadyForReview) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) WorkflowState);
      }
      if (IsTerminal != false) {
        size += 1 + 1;
      }
      size += reasons_.CalculateSize(_repeated_reasons_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(WorkflowTransition other) {
      if (other == null) {
        return;
      }
      if (other.Value != 0) {
        Value = other.Value;
      }
      if (other.DisplayName.Length != 0) {
        DisplayName = other.DisplayName;
      }
      if (other.WorkflowState != global::Events.Integration.Protobuf.Workflow.Common.WorkflowState.ReadyForReview) {
        WorkflowState = other.WorkflowState;
      }
      if (other.IsTerminal != false) {
        IsTerminal = other.IsTerminal;
      }
      reasons_.Add(other.reasons_);
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
          case 8: {
            Value = input.ReadInt32();
            break;
          }
          case 18: {
            DisplayName = input.ReadString();
            break;
          }
          case 24: {
            WorkflowState = (global::Events.Integration.Protobuf.Workflow.Common.WorkflowState) input.ReadEnum();
            break;
          }
          case 32: {
            IsTerminal = input.ReadBool();
            break;
          }
          case 42: {
            reasons_.AddEntriesFrom(input, _repeated_reasons_codec);
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
          case 8: {
            Value = input.ReadInt32();
            break;
          }
          case 18: {
            DisplayName = input.ReadString();
            break;
          }
          case 24: {
            WorkflowState = (global::Events.Integration.Protobuf.Workflow.Common.WorkflowState) input.ReadEnum();
            break;
          }
          case 32: {
            IsTerminal = input.ReadBool();
            break;
          }
          case 42: {
            reasons_.AddEntriesFrom(ref input, _repeated_reasons_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class TransitionReason : pb::IMessage<TransitionReason>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<TransitionReason> _parser = new pb::MessageParser<TransitionReason>(() => new TransitionReason());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TransitionReason> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransitionReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TransitionReason() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TransitionReason(TransitionReason other) : this() {
      id_ = other.id_;
      reason_ = other.reason_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TransitionReason Clone() {
      return new TransitionReason(this);
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "Reason" field.</summary>
    public const int ReasonFieldNumber = 2;
    private string reason_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Reason {
      get { return reason_; }
      set {
        reason_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TransitionReason);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TransitionReason other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Reason != other.Reason) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Reason.Length != 0) hash ^= Reason.GetHashCode();
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
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Reason.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Reason);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Reason.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Reason);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Reason.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Reason);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TransitionReason other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Reason.Length != 0) {
        Reason = other.Reason;
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
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Reason = input.ReadString();
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
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Reason = input.ReadString();
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