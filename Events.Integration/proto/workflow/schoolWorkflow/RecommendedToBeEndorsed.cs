// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: workflow/schoolWorkflow/RecommendedToBeEndorsed.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Events.Integration.Protobuf.Workflow.SchoolWorkflow {

  /// <summary>Holder for reflection information generated from workflow/schoolWorkflow/RecommendedToBeEndorsed.proto</summary>
  public static partial class RecommendedToBeEndorsedReflection {

    #region Descriptor
    /// <summary>File descriptor for workflow/schoolWorkflow/RecommendedToBeEndorsed.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RecommendedToBeEndorsedReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjV3b3JrZmxvdy9zY2hvb2xXb3JrZmxvdy9SZWNvbW1lbmRlZFRvQmVFbmRv",
            "cnNlZC5wcm90bxoWcHJvdG9idWYtbmV0L2JjbC5wcm90bxofZ29vZ2xlL3By",
            "b3RvYnVmL3RpbWVzdGFtcC5wcm90bxood29ya2Zsb3cvY29tbW9uL1dvcmtm",
            "bG93VHJhbnNpdGlvbi5wcm90byL2AQoXUmVjb21tZW5kZWRUb0JlRW5kb3Jz",
            "ZWQSIwoQQnVzaW5lc3NFbnRpdHlJZBgBIAEoCzIJLmJjbC5HdWlkEhoKEkJ1",
            "c2luZXNzRW50aXR5VHlwZRgCIAEoCRIQCghVc2VybmFtZRgDIAEoCRIzCg9B",
            "Y3Rpb25UaW1lc3RhbXAYBCABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0",
            "YW1wEkIKFVZhbGlkU3RhdGVUcmFuc2l0aW9ucxgFIAMoCzIjLndvcmtmbG93",
            "LmNvbW1vbi5Xb3JrZmxvd1RyYW5zaXRpb24SDwoHQ29tbWVudBgGIAEoCUI2",
            "qgIzRXZlbnRzLkludGVncmF0aW9uLlByb3RvYnVmLldvcmtmbG93LlNjaG9v",
            "bFdvcmtmbG93YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::ProtoBuf.Bcl.BclReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, global::Events.Integration.Protobuf.Workflow.Common.WorkflowTransitionReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.SchoolWorkflow.RecommendedToBeEndorsed), global::Events.Integration.Protobuf.Workflow.SchoolWorkflow.RecommendedToBeEndorsed.Parser, new[]{ "BusinessEntityId", "BusinessEntityType", "Username", "ActionTimestamp", "ValidStateTransitions", "Comment" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///Workflow.RecommendedToBeEndorsed
  /// </summary>
  public sealed partial class RecommendedToBeEndorsed : pb::IMessage<RecommendedToBeEndorsed>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<RecommendedToBeEndorsed> _parser = new pb::MessageParser<RecommendedToBeEndorsed>(() => new RecommendedToBeEndorsed());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RecommendedToBeEndorsed> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Events.Integration.Protobuf.Workflow.SchoolWorkflow.RecommendedToBeEndorsedReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RecommendedToBeEndorsed() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RecommendedToBeEndorsed(RecommendedToBeEndorsed other) : this() {
      businessEntityId_ = other.businessEntityId_ != null ? other.businessEntityId_.Clone() : null;
      businessEntityType_ = other.businessEntityType_;
      username_ = other.username_;
      actionTimestamp_ = other.actionTimestamp_ != null ? other.actionTimestamp_.Clone() : null;
      validStateTransitions_ = other.validStateTransitions_.Clone();
      comment_ = other.comment_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RecommendedToBeEndorsed Clone() {
      return new RecommendedToBeEndorsed(this);
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

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RecommendedToBeEndorsed);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RecommendedToBeEndorsed other) {
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
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RecommendedToBeEndorsed other) {
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
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
