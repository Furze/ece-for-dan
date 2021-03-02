// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: workflow/schoolWorkflow/Retracted.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Events.Integration.Protobuf.Workflow.SchoolWorkflow {

  /// <summary>Holder for reflection information generated from workflow/schoolWorkflow/Retracted.proto</summary>
  public static partial class RetractedReflection {

    #region Descriptor
    /// <summary>File descriptor for workflow/schoolWorkflow/Retracted.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RetractedReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cid3b3JrZmxvdy9zY2hvb2xXb3JrZmxvdy9SZXRyYWN0ZWQucHJvdG8aFnBy",
            "b3RvYnVmLW5ldC9iY2wucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3Rh",
            "bXAucHJvdG8aKHdvcmtmbG93L2NvbW1vbi9Xb3JrZmxvd1RyYW5zaXRpb24u",
            "cHJvdG8i/AEKCVJldHJhY3RlZBIjChBCdXNpbmVzc0VudGl0eUlkGAEgASgL",
            "MgkuYmNsLkd1aWQSGgoSQnVzaW5lc3NFbnRpdHlUeXBlGAIgASgJEhAKCFVz",
            "ZXJuYW1lGAMgASgJEjMKD0FjdGlvblRpbWVzdGFtcBgEIAEoCzIaLmdvb2ds",
            "ZS5wcm90b2J1Zi5UaW1lc3RhbXASQgoVVmFsaWRTdGF0ZVRyYW5zaXRpb25z",
            "GAUgAygLMiMud29ya2Zsb3cuY29tbW9uLldvcmtmbG93VHJhbnNpdGlvbhIP",
            "CgdDb21tZW50GAYgASgJEhIKCldvcmtmbG93SWQYByABKAVCNqoCM0V2ZW50",
            "cy5JbnRlZ3JhdGlvbi5Qcm90b2J1Zi5Xb3JrZmxvdy5TY2hvb2xXb3JrZmxv",
            "d2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::ProtoBuf.Bcl.BclReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransitionReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.SchoolWorkflow.Retracted), global::Events.Integration.Protobuf.Workflow.SchoolWorkflow.Retracted.Parser, new[]{ "BusinessEntityId", "BusinessEntityType", "Username", "ActionTimestamp", "ValidStateTransitions", "Comment", "WorkflowId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Retracted : pb::IMessage<Retracted>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Retracted> _parser = new pb::MessageParser<Retracted>(() => new Retracted());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Retracted> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Workflow.SchoolWorkflow.RetractedReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Retracted() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Retracted(Retracted other) : this() {
      businessEntityId_ = other.businessEntityId_ != null ? other.businessEntityId_.Clone() : null;
      businessEntityType_ = other.businessEntityType_;
      username_ = other.username_;
      actionTimestamp_ = other.actionTimestamp_ != null ? other.actionTimestamp_.Clone() : null;
      validStateTransitions_ = other.validStateTransitions_.Clone();
      comment_ = other.comment_;
      workflowId_ = other.workflowId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Retracted Clone() {
      return new Retracted(this);
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

    /// <summary>Field number for the "BusinessEntityType" field.</summary>
    public const int BusinessEntityTypeFieldNumber = 2;
    private string businessEntityType_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string BusinessEntityType {
      get { return businessEntityType_; }
      set {
        businessEntityType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Username" field.</summary>
    public const int UsernameFieldNumber = 3;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ActionTimestamp" field.</summary>
    public const int ActionTimestampFieldNumber = 4;
    private global::Google.Protobuf.WellKnownTypes.Timestamp actionTimestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Timestamp ActionTimestamp {
      get { return actionTimestamp_; }
      set {
        actionTimestamp_ = value;
      }
    }

    /// <summary>Field number for the "ValidStateTransitions" field.</summary>
    public const int ValidStateTransitionsFieldNumber = 5;
    private static readonly pb::FieldCodec<global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition> _repeated_validStateTransitions_codec
        = pb::FieldCodec.ForMessage(42, global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition.Parser);
    private readonly pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition> validStateTransitions_ = new pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransition> ValidStateTransitions {
      get { return validStateTransitions_; }
    }

    /// <summary>Field number for the "Comment" field.</summary>
    public const int CommentFieldNumber = 6;
    private string comment_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Comment {
      get { return comment_; }
      set {
        comment_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "WorkflowId" field.</summary>
    public const int WorkflowIdFieldNumber = 7;
    private int workflowId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int WorkflowId {
      get { return workflowId_; }
      set {
        workflowId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Retracted);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Retracted other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(BusinessEntityId, other.BusinessEntityId)) return false;
      if (BusinessEntityType != other.BusinessEntityType) return false;
      if (Username != other.Username) return false;
      if (!object.Equals(ActionTimestamp, other.ActionTimestamp)) return false;
      if(!validStateTransitions_.Equals(other.validStateTransitions_)) return false;
      if (Comment != other.Comment) return false;
      if (WorkflowId != other.WorkflowId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (businessEntityId_ != null) hash ^= BusinessEntityId.GetHashCode();
      if (BusinessEntityType.Length != 0) hash ^= BusinessEntityType.GetHashCode();
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (actionTimestamp_ != null) hash ^= ActionTimestamp.GetHashCode();
      hash ^= validStateTransitions_.GetHashCode();
      if (Comment.Length != 0) hash ^= Comment.GetHashCode();
      if (WorkflowId != 0) hash ^= WorkflowId.GetHashCode();
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
      if (BusinessEntityType.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(BusinessEntityType);
      }
      if (Username.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Username);
      }
      if (actionTimestamp_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(ActionTimestamp);
      }
      validStateTransitions_.WriteTo(output, _repeated_validStateTransitions_codec);
      if (Comment.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Comment);
      }
      if (WorkflowId != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(WorkflowId);
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
      if (BusinessEntityType.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(BusinessEntityType);
      }
      if (Username.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Username);
      }
      if (actionTimestamp_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(ActionTimestamp);
      }
      validStateTransitions_.WriteTo(ref output, _repeated_validStateTransitions_codec);
      if (Comment.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Comment);
      }
      if (WorkflowId != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(WorkflowId);
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
      if (BusinessEntityType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BusinessEntityType);
      }
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (actionTimestamp_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ActionTimestamp);
      }
      size += validStateTransitions_.CalculateSize(_repeated_validStateTransitions_codec);
      if (Comment.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Comment);
      }
      if (WorkflowId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(WorkflowId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Retracted other) {
      if (other == null) {
        return;
      }
      if (other.businessEntityId_ != null) {
        if (businessEntityId_ == null) {
          BusinessEntityId = new global::ProtoBuf.Bcl.Guid();
        }
        BusinessEntityId.MergeFrom(other.BusinessEntityId);
      }
      if (other.BusinessEntityType.Length != 0) {
        BusinessEntityType = other.BusinessEntityType;
      }
      if (other.Username.Length != 0) {
        Username = other.Username;
      }
      if (other.actionTimestamp_ != null) {
        if (actionTimestamp_ == null) {
          ActionTimestamp = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        ActionTimestamp.MergeFrom(other.ActionTimestamp);
      }
      validStateTransitions_.Add(other.validStateTransitions_);
      if (other.Comment.Length != 0) {
        Comment = other.Comment;
      }
      if (other.WorkflowId != 0) {
        WorkflowId = other.WorkflowId;
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
          case 18: {
            BusinessEntityType = input.ReadString();
            break;
          }
          case 26: {
            Username = input.ReadString();
            break;
          }
          case 34: {
            if (actionTimestamp_ == null) {
              ActionTimestamp = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(ActionTimestamp);
            break;
          }
          case 42: {
            validStateTransitions_.AddEntriesFrom(input, _repeated_validStateTransitions_codec);
            break;
          }
          case 50: {
            Comment = input.ReadString();
            break;
          }
          case 56: {
            WorkflowId = input.ReadInt32();
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
          case 18: {
            BusinessEntityType = input.ReadString();
            break;
          }
          case 26: {
            Username = input.ReadString();
            break;
          }
          case 34: {
            if (actionTimestamp_ == null) {
              ActionTimestamp = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(ActionTimestamp);
            break;
          }
          case 42: {
            validStateTransitions_.AddEntriesFrom(ref input, _repeated_validStateTransitions_codec);
            break;
          }
          case 50: {
            Comment = input.ReadString();
            break;
          }
          case 56: {
            WorkflowId = input.ReadInt32();
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
