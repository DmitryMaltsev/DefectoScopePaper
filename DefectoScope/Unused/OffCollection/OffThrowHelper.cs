#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

#endregion

namespace DefectoScope.OffCollection
{
    internal static class OffThrowHelper
    {
        internal static string GetResourceString(string key, params object[] values) =>
            string.Format(CultureInfo.CurrentCulture, key, values);

        internal static void ThrowArgumentOutOfRangeException()
        {
            ThrowArgumentOutOfRangeException(ExceptionArgument.Index, ExceptionResource.ArgumentOutOfRangeIndex);
        }

        internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
        {
            throw new ArgumentException(GetResourceString("Arg_WrongType", key, targetType), nameof(key));
        }

        internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
        {
            throw new ArgumentException(GetResourceString("Arg_WrongType", value, targetType), nameof(value));
        }

        internal static void ThrowKeyNotFoundException()
        {
            throw new KeyNotFoundException();
        }

        internal static void ThrowArgumentException(ExceptionResource resource)
        {
            throw new ArgumentException(GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
        {
            throw new ArgumentException(GetResourceString(GetResourceName(resource)), GetArgumentName(argument));
        }

        internal static void ThrowArgumentNullException(ExceptionArgument argument)
        {
            throw new ArgumentNullException(GetArgumentName(argument));
        }

        internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
        {
            throw new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
        {
            throw new ArgumentOutOfRangeException(GetArgumentName(argument),
                GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowInvalidOperationException(ExceptionResource resource)
        {
            throw new InvalidOperationException(GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowSerializationException(ExceptionResource resource)
        {
            throw new SerializationException(GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowSecurityException(ExceptionResource resource)
        {
            throw new SecurityException(GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowNotSupportedException(ExceptionResource resource)
        {
            throw new NotSupportedException(GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowUnauthorizedAccessException(ExceptionResource resource)
        {
            throw new UnauthorizedAccessException(GetResourceString(GetResourceName(resource)));
        }

        internal static void ThrowObjectDisposedException(string objectName, ExceptionResource resource)
        {
            throw new ObjectDisposedException(objectName, GetResourceString(GetResourceName(resource)));
        }

        // Allow nulls for reference types and Nullable<U>, but not for value types.
        internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
        {
            // Note that default(T) is not equal to null for value types except when T is Nullable<U>. 
            if (value == null && default(T) != null)
                ThrowArgumentNullException(argName);
        }

        //
        // This function will convert an ExceptionArgument enum value to the argument name string.
        //
        internal static string GetArgumentName(ExceptionArgument argument)
        {
            string argumentName;

            switch (argument)
            {
                case ExceptionArgument.Array:
                    argumentName = "array";
                    break;

                case ExceptionArgument.ArrayIndex:
                    argumentName = "arrayIndex";
                    break;

                case ExceptionArgument.Capacity:
                    argumentName = "capacity";
                    break;

                case ExceptionArgument.Collection:
                    argumentName = "collection";
                    break;

                case ExceptionArgument.List:
                    argumentName = "list";
                    break;

                case ExceptionArgument.Converter:
                    argumentName = "converter";
                    break;

                case ExceptionArgument.Count:
                    argumentName = "count";
                    break;

                case ExceptionArgument.Dictionary:
                    argumentName = "dictionary";
                    break;

                case ExceptionArgument.DictionaryCreationThreshold:
                    argumentName = "dictionaryCreationThreshold";
                    break;

                case ExceptionArgument.Index:
                    argumentName = "index";
                    break;

                case ExceptionArgument.Info:
                    argumentName = "info";
                    break;

                case ExceptionArgument.Key:
                    argumentName = "key";
                    break;

                case ExceptionArgument.Match:
                    argumentName = "match";
                    break;

                case ExceptionArgument.Obj:
                    argumentName = "obj";
                    break;

                case ExceptionArgument.Queue:
                    argumentName = "queue";
                    break;

                case ExceptionArgument.Stack:
                    argumentName = "stack";
                    break;

                case ExceptionArgument.StartIndex:
                    argumentName = "startIndex";
                    break;

                case ExceptionArgument.Value:
                    argumentName = "value";
                    break;

                case ExceptionArgument.Name:
                    argumentName = "name";
                    break;

                case ExceptionArgument.Mode:
                    argumentName = "mode";
                    break;

                case ExceptionArgument.Item:
                    argumentName = "item";
                    break;

                case ExceptionArgument.Options:
                    argumentName = "options";
                    break;

                case ExceptionArgument.View:
                    argumentName = "view";
                    break;

                case ExceptionArgument.SourceBytesToCopy:
                    argumentName = "sourceBytesToCopy";
                    break;

                default:
                    Contract.Assert(false, "The enum value is not defined, please checked ExceptionArgumentName Enum.");
                    return string.Empty;
            }

            return argumentName;
        }

        //
        // This function will convert an ExceptionResource enum value to the resource string.
        //
        internal static string GetResourceName(ExceptionResource resource)
        {
            string resourceName;

            switch (resource)
            {
                case ExceptionResource.ArgumentImplementIComparable:
                    resourceName = "Argument_ImplementIComparable";
                    break;

                case ExceptionResource.ArgumentAddingDuplicate:
                    resourceName = "Argument_AddingDuplicate";
                    break;

                case ExceptionResource.ArgumentOutOfRangeBiggerThanCollection:
                    resourceName = "ArgumentOutOfRange_BiggerThanCollection";
                    break;

                case ExceptionResource.ArgumentOutOfRangeCount:
                    resourceName = "ArgumentOutOfRange_Count";
                    break;

                case ExceptionResource.ArgumentOutOfRangeIndex:
                    resourceName = "ArgumentOutOfRange_Index";
                    break;

                case ExceptionResource.ArgumentOutOfRangeInvalidThreshold:
                    resourceName = "ArgumentOutOfRange_InvalidThreshold";
                    break;

                case ExceptionResource.ArgumentOutOfRangeListInsert:
                    resourceName = "ArgumentOutOfRange_ListInsert";
                    break;

                case ExceptionResource.ArgumentOutOfRangeNeedNonNegNum:
                    resourceName = "ArgumentOutOfRange_NeedNonNegNum";
                    break;

                case ExceptionResource.ArgumentOutOfRangeSmallCapacity:
                    resourceName = "ArgumentOutOfRange_SmallCapacity";
                    break;

                case ExceptionResource.ArgArrayPlusOffTooSmall:
                    resourceName = "Arg_ArrayPlusOffTooSmall";
                    break;

                case ExceptionResource.ArgRankMultiDimNotSupported:
                    resourceName = "Arg_RankMultiDimNotSupported";
                    break;

                case ExceptionResource.ArgNonZeroLowerBound:
                    resourceName = "Arg_NonZeroLowerBound";
                    break;

                case ExceptionResource.ArgumentInvalidArrayType:
                    resourceName = "Argument_InvalidArrayType";
                    break;

                case ExceptionResource.ArgumentInvalidOffLen:
                    resourceName = "Argument_InvalidOffLen";
                    break;

                case ExceptionResource.ArgumentItemNotExist:
                    resourceName = "Argument_ItemNotExist";
                    break;

                case ExceptionResource.InvalidOperationCannotRemoveFromStackOrQueue:
                    resourceName = "InvalidOperation_CannotRemoveFromStackOrQueue";
                    break;

                case ExceptionResource.InvalidOperationEmptyQueue:
                    resourceName = "InvalidOperation_EmptyQueue";
                    break;

                case ExceptionResource.InvalidOperationEnumOpCantHappen:
                    resourceName = "InvalidOperation_EnumOpCantHappen";
                    break;

                case ExceptionResource.InvalidOperationEnumFailedVersion:
                    resourceName = "InvalidOperation_EnumFailedVersion";
                    break;

                case ExceptionResource.InvalidOperationEmptyStack:
                    resourceName = "InvalidOperation_EmptyStack";
                    break;

                case ExceptionResource.InvalidOperationEnumNotStarted:
                    resourceName = "InvalidOperation_EnumNotStarted";
                    break;

                case ExceptionResource.InvalidOperationEnumEnded:
                    resourceName = "InvalidOperation_EnumEnded";
                    break;

                case ExceptionResource.NotSupportedKeyCollectionSet:
                    resourceName = "NotSupported_KeyCollectionSet";
                    break;

                case ExceptionResource.NotSupportedReadOnlyCollection:
                    resourceName = "NotSupported_ReadOnlyCollection";
                    break;

                case ExceptionResource.NotSupportedValueCollectionSet:
                    resourceName = "NotSupported_ValueCollectionSet";
                    break;


                case ExceptionResource.NotSupportedSortedListNestedWrite:
                    resourceName = "NotSupported_SortedListNestedWrite";
                    break;


                case ExceptionResource.SerializationInvalidOnDeser:
                    resourceName = "Serialization_InvalidOnDeser";
                    break;

                case ExceptionResource.SerializationMissingKeys:
                    resourceName = "Serialization_MissingKeys";
                    break;

                case ExceptionResource.SerializationNullKey:
                    resourceName = "Serialization_NullKey";
                    break;

                case ExceptionResource.ArgumentInvalidType:
                    resourceName = "Argument_InvalidType";
                    break;

                case ExceptionResource.ArgumentInvalidArgumentForComparison:
                    resourceName = "Argument_InvalidArgumentForComparison";
                    break;

                case ExceptionResource.InvalidOperationNoValue:
                    resourceName = "InvalidOperation_NoValue";
                    break;

                case ExceptionResource.InvalidOperationRegRemoveSubKey:
                    resourceName = "InvalidOperation_RegRemoveSubKey";
                    break;

                case ExceptionResource.ArgRegSubKeyAbsent:
                    resourceName = "Arg_RegSubKeyAbsent";
                    break;

                case ExceptionResource.ArgRegSubKeyValueAbsent:
                    resourceName = "Arg_RegSubKeyValueAbsent";
                    break;

                case ExceptionResource.ArgRegKeyDelHive:
                    resourceName = "Arg_RegKeyDelHive";
                    break;

                case ExceptionResource.SecurityRegistryPermission:
                    resourceName = "Security_RegistryPermission";
                    break;

                case ExceptionResource.ArgRegSetStrArrNull:
                    resourceName = "Arg_RegSetStrArrNull";
                    break;

                case ExceptionResource.ArgRegSetMismatchedKind:
                    resourceName = "Arg_RegSetMismatchedKind";
                    break;

                case ExceptionResource.UnauthorizedAccessRegistryNoWrite:
                    resourceName = "UnauthorizedAccess_RegistryNoWrite";
                    break;

                case ExceptionResource.ObjectDisposedRegKeyClosed:
                    resourceName = "ObjectDisposed_RegKeyClosed";
                    break;

                case ExceptionResource.ArgRegKeyStrLenBug:
                    resourceName = "Arg_RegKeyStrLenBug";
                    break;

                case ExceptionResource.ArgumentInvalidRegistryKeyPermissionCheck:
                    resourceName = "Argument_InvalidRegistryKeyPermissionCheck";
                    break;

                case ExceptionResource.NotSupportedInComparableType:
                    resourceName = "NotSupported_InComparableType";
                    break;

                case ExceptionResource.ArgumentInvalidRegistryOptionsCheck:
                    resourceName = "Argument_InvalidRegistryOptionsCheck";
                    break;

                case ExceptionResource.ArgumentInvalidRegistryViewCheck:
                    resourceName = "Argument_InvalidRegistryViewCheck";
                    break;

                default:
                    Contract.Assert(false, "The enum value is not defined, please checked ExceptionArgumentName Enum.");
                    return string.Empty;
            }

            return resourceName;
        }
    }

    //
    // The convention for this enum is using the argument name as the enum name
    // 
    internal enum ExceptionArgument
    {
        Obj,
        Dictionary,
        DictionaryCreationThreshold,
        Array,
        Info,
        Key,
        Collection,
        List,
        Match,
        Converter,
        Queue,
        Stack,
        Capacity,
        Index,
        StartIndex,
        Value,
        Count,
        ArrayIndex,
        Name,
        Mode,
        Item,
        Options,
        View,
        SourceBytesToCopy
    }

    //
    // The convention for this enum is using the resource name as the enum name
    // 
    internal enum ExceptionResource
    {
        ArgumentImplementIComparable,
        ArgumentInvalidType,
        ArgumentInvalidArgumentForComparison,
        ArgumentInvalidRegistryKeyPermissionCheck,
        ArgumentOutOfRangeNeedNonNegNum,

        ArgArrayPlusOffTooSmall,
        ArgNonZeroLowerBound,
        ArgRankMultiDimNotSupported,
        ArgRegKeyDelHive,
        ArgRegKeyStrLenBug,
        ArgRegSetStrArrNull,
        ArgRegSetMismatchedKind,
        ArgRegSubKeyAbsent,
        ArgRegSubKeyValueAbsent,

        ArgumentAddingDuplicate,
        SerializationInvalidOnDeser,
        SerializationMissingKeys,
        SerializationNullKey,
        ArgumentInvalidArrayType,
        NotSupportedKeyCollectionSet,
        NotSupportedValueCollectionSet,
        ArgumentOutOfRangeSmallCapacity,
        ArgumentOutOfRangeIndex,
        ArgumentInvalidOffLen,
        ArgumentItemNotExist,
        ArgumentOutOfRangeCount,
        ArgumentOutOfRangeInvalidThreshold,
        ArgumentOutOfRangeListInsert,
        NotSupportedReadOnlyCollection,
        InvalidOperationCannotRemoveFromStackOrQueue,
        InvalidOperationEmptyQueue,
        InvalidOperationEnumOpCantHappen,
        InvalidOperationEnumFailedVersion,
        InvalidOperationEmptyStack,
        ArgumentOutOfRangeBiggerThanCollection,
        InvalidOperationEnumNotStarted,
        InvalidOperationEnumEnded,
        NotSupportedSortedListNestedWrite,
        InvalidOperationNoValue,
        InvalidOperationRegRemoveSubKey,
        SecurityRegistryPermission,
        UnauthorizedAccessRegistryNoWrite,
        ObjectDisposedRegKeyClosed,
        NotSupportedInComparableType,
        ArgumentInvalidRegistryOptionsCheck,
        ArgumentInvalidRegistryViewCheck
    }
}