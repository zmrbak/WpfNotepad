using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WpfNotepad.Interactivity
{
    [DebuggerNonUserCode]
    [CompilerGenerated]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class ExceptionStringTable
    {
        internal ExceptionStringTable()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(ExceptionStringTable.resourceMan, null))
                {
                    ResourceManager resourceManager = new ResourceManager("ExceptionStringTable", typeof(ExceptionStringTable).Assembly);
                    ExceptionStringTable.resourceMan = resourceManager;
                }
                return ExceptionStringTable.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return ExceptionStringTable.resourceCulture;
            }
            set
            {
                ExceptionStringTable.resourceCulture = value;
            }
        }

        internal static string CannotHostBehaviorCollectionMultipleTimesExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("CannotHostBehaviorCollectionMultipleTimesExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string CannotHostBehaviorMultipleTimesExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("CannotHostBehaviorMultipleTimesExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string CannotHostTriggerActionMultipleTimesExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("CannotHostTriggerActionMultipleTimesExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string CannotHostTriggerCollectionMultipleTimesExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("CannotHostTriggerCollectionMultipleTimesExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string CannotHostTriggerMultipleTimesExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("CannotHostTriggerMultipleTimesExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string CommandDoesNotExistOnBehaviorWarningMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("CommandDoesNotExistOnBehaviorWarningMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string DuplicateItemInCollectionExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("DuplicateItemInCollectionExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string EventTriggerBaseInvalidEventExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("EventTriggerBaseInvalidEventExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string EventTriggerCannotFindEventNameExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("EventTriggerCannotFindEventNameExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string RetargetedTypeConstraintViolatedExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("RetargetedTypeConstraintViolatedExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string TypeConstraintViolatedExceptionMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("TypeConstraintViolatedExceptionMessage", ExceptionStringTable.resourceCulture);
            }
        }

        internal static string UnableToResolveTargetNameWarningMessage
        {
            get
            {
                return ExceptionStringTable.ResourceManager.GetString("UnableToResolveTargetNameWarningMessage", ExceptionStringTable.resourceCulture);
            }
        }

        private static ResourceManager resourceMan;

        private static CultureInfo resourceCulture;
    }
}