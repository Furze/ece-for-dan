// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: workflow/RecommendedToBeEndorsed.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Events.Integration.Protobuf.Workflow {

  /// <summary>Holder for reflection information generated from workflow/RecommendedToBeEndorsed.proto</summary>
  public static partial class RecommendedToBeEndorsedReflection {

    #region Descriptor
    /// <summary>File descriptor for workflow/RecommendedToBeEndorsed.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RecommendedToBeEndorsedReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiZ3b3JrZmxvdy9SZWNvbW1lbmRlZFRvQmVFbmRvcnNlZC5wcm90bxoWcHJv",
            "dG9idWYtbmV0L2JjbC5wcm90bxofZ29vZ2xlL3Byb3RvYnVmL3RpbWVzdGFt",
            "cC5wcm90byKhAQoXUmVjb21tZW5kZWRUb0JlRW5kb3JzZWQSIwoQQnVzaW5l",
            "c3NFbnRpdHlJZBgBIAEoCzIJLmJjbC5HdWlkEhoKEkJ1c2luZXNzRW50aXR5",
            "VHlwZRgCIAEoCRIQCghVc2VybmFtZRgDIAEoCRIzCg9BY3Rpb25UaW1lc3Rh",
            "bXAYBCABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wQieqAiRFdmVu",
            "dHMuSW50ZWdyYXRpb24uUHJvdG9idWYuV29ya2Zsb3diBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::ProtoBuf.Bcl.BclReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Events.Integration.Protobuf.Workflow.RecommendedToBeEndorsed), global::Events.Integration.Protobuf.Workflow.RecommendedToBeEndorsed.Parser, new[]{ "BusinessEntityId", "BusinessEntityType", "Username", "ActionTimestamp" }, null, null, null, null)
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
      get { return global::Events.Integration.Protobuf.Workflow.RecommendedToBeEndorsedReflection.Descriptor.MessageTypes[0]; }
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
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (businessEntityId_ != null) hash ^= BusinessEntityId.GetHashCode();
      if (BusinessEntityType.Length != 0) hash ^= BusinessEntityType.GetHashCode();
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (actionTimestamp_ != null) hash ^= ActionTimestamp.GetHashCode();
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
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
