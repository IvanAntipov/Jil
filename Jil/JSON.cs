using System;
using System.IO;
using JilFork.Common;
using JilFork.Deserialize;
using JilFork.DeserializeDynamic;
using JilFork.Serialize;
using JilFork.SerializeDynamic;
using MicrosoftStyle = JilFork.Serialize.MicrosoftStyle;
using MicrosoftStyleCamelCase = JilFork.Serialize.MicrosoftStyleCamelCase;

namespace JilFork
{
    /// <summary>
    /// Fast JSON serializer.
    /// </summary>
    public sealed class JSON
    {
        static Options DefaultOptions = Options.Default;

        /// <summary>
        /// Sets the Options object that Jil will use to calls of Serialize(Dynamic) and Deserialize(Dynamic)
        /// if no Options object is provided.
        /// 
        /// By default, Jil will use the Options.Default object.
        /// 
        /// The current default Options can be retrieved with GetDefaultOptions().
        /// </summary>
        public static void SetDefaultOptions(Options options)
        {
            if (options == null) throw new ArgumentNullException("options");

            DefaultOptions = options;
        }

        /// <summary>
        /// Gets the Options object that Jil will use to calls of Serialize(Dynamic) and Deserialize(Dynamic)
        /// if no Options object is provided.
        /// 
        /// By default, Jil will use the Options.Default object.
        /// 
        /// The default Options can be set with SetDefaultOptions(Options options).
        /// </summary>
        public static Options GetDefaultOptions()
        {
            return DefaultOptions;
        }

        /// <summary>
        /// Serializes the given data to the provided TextWriter.
        /// 
        /// Pass an Options object to configure the particulars (such as whitespace, and DateTime formats) of
        /// the produced JSON.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// 
        /// Unlike Serialize, this method will inspect the Type of data to determine what serializer to invoke.
        /// This is not as fast as calling Serialize with a known type.
        /// 
        /// Objects with participate in the DLR will be serialized appropriately, all other types
        /// will be serialized via reflection.
        /// </summary>
        public static void SerializeDynamic(dynamic data, TextWriter output, Options options = null)
        {
            DynamicSerializer.Serialize(output, (object)data, options ?? DefaultOptions, 0);
        }

        /// <summary>
        /// Serializes the given data, returning it as a string.
        /// 
        /// Pass an Options object to configure the particulars (such as whitespace, and DateTime formats) of
        /// the produced JSON.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// 
        /// Unlike Serialize, this method will inspect the Type of data to determine what serializer to invoke.
        /// This is not as fast as calling Serialize with a known type.
        /// 
        /// Objects with participate in the DLR will be serialized appropriately, all other types
        /// will be serialized via reflection.
        /// </summary>
        public static string SerializeDynamic(object data, Options options = null)
        {
            using (var str = new StringWriter())
            {
                SerializeDynamic(data, str, options);
                return str.ToString();
            }
        }

        /// <summary>
        /// Serializes the given data to the provided TextWriter.
        /// 
        /// Pass an Options object to configure the particulars (such as whitespace, and DateTime formats) of
        /// the produced JSON.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// </summary>
        public static void Serialize<T>(T data, TextWriter output, Options options = null)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            if (typeof(T) == typeof(object))
            {
                SerializeDynamic(data, output, options);
                return;
            }

            options = options ?? DefaultOptions;

            GetWriterAction<T>(options)(output, data, 0);
        }

        /// <summary>
        /// Generated giant switch of option finding via OptionsGeneration.linq
        /// </summary>
        static Action<TextWriter, T, int> GetWriterAction<T>(Options options)
        {
            // Start OptionsGeneration.linq generated content: GetWriterAction
            switch (options.UseDateTimeFormat)
            {
                case DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyle, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrint, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.MillisecondsSinceUnixEpoch:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<Milliseconds, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrint, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.SecondsSinceUnixEpoch:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<Seconds, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrint, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.ISO8601:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601CamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601Utc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601UtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601Inherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601InheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601InheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601InheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrint, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.RFC1123:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123CamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123Utc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123UtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123Inherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123InheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123InheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123InheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrint, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNulls, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONP, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInherited, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.Get();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInheritedUtc, T>.Get();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.Get();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
            // End OptionsGeneration.linq generated content: GetWriterAction

            throw new InvalidOperationException("Unexpected Options: " + options);
        }


        /// <summary>
        /// Generated giant switch of option finding via OptionsGeneration.linq
        /// </summary>
        static Serialize.StringThunkDelegate<T> GetThunkerDelegate<T>(Options options)
        {
            // Start OptionsGeneration.linq generated content: GetThunkerDelegate 
switch (options.UseDateTimeFormat)
            {
                case DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyle, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStyleExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrint, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.MillisecondsSinceUnixEpoch:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<Milliseconds, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrint, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<MillisecondsPrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.SecondsSinceUnixEpoch:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<Seconds, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrint, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<SecondsPrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.ISO8601:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601CamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601Utc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601UtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601Inherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601InheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601InheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601InheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601JSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601ExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrint, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<ISO8601PrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case DateTimeFormat.RFC1123:
                    switch (options.ShouldPrettyPrint)
                    {
                        case false:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123CamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123Utc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123UtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123Inherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123InheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123InheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123InheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123JSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123ExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case true:
                            switch (options.ShouldExcludeNulls)
                            {
                                case false:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrint, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case true:
                                    switch (options.IsJSONP)
                                    {
                                        case false:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNulls, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case true:
                                            switch (options.ShouldIncludeInherited)
                                            {
                                                case false:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONP, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case true:
                                                    switch (options.UseUnspecifiedDateTimeKindBehavior)
                                                    {
                                                        case UnspecifiedDateTimeKindBehavior.IsLocal:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInherited, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInheritedCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                        case UnspecifiedDateTimeKindBehavior.IsUTC:
                                                            switch (options.SerializationNameFormat)
                                                            {
                                                                case SerializationNameFormat.Verbatim:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInheritedUtc, T>.GetToString();
                                                                case SerializationNameFormat.CamelCase:
                                                                    return JilFork.Serialize.TypeCache<RFC1123PrettyPrintExcludeNullsJSONPInheritedUtcCamelCase, T>.GetToString();
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
            // End OptionsGeneration.linq generated content: GetThunkerDelegate

            throw new InvalidOperationException("Unexpected Options: " + options);
        }

        /// <summary>
        /// Serializes the given data, returning the output as a string.
        /// 
        /// Pass an Options object to configure the particulars (such as whitespace, and DateTime formats) of
        /// the produced JSON.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// </summary>
        public static string Serialize<T>(T data, Options options = null)
        {
            if (typeof(T) == typeof(object))
            {
                return SerializeDynamic(data, options);
            }

            options = options ?? DefaultOptions;


            var writer = new ThunkWriter();
            writer.Init();
            GetThunkerDelegate<T>(options)(ref writer, data, 0);
            return writer.StaticToString();
        }

        /// <summary>
        /// Deserializes JSON from the given TextReader as the passed type.
        /// 
        /// This is equivalent to calling Deserialize&lt;T&gt;(TextReader, Options), except
        /// without requiring a generic parameter.  For true dynamic deserialization, you 
        /// should use DeserializeDynamic instead.
        /// 
        /// Pass an Options object to specify the particulars (such as DateTime formats) of
        /// the JSON being deserialized.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// </summary>
        public static object Deserialize(TextReader reader, Type type, Options options = null)
        {
            if(reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if(type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type == typeof(object))
            {
                return DeserializeDynamic(reader, options);
            }

            return DeserializeIndirect.DeserializeFromStream(reader.MakeSupportPeek(), type, options);
        }

        /// <summary>
        /// Deserializes JSON from the given string as the passed type.
        /// 
        /// This is equivalent to calling Deserialize&lt;T&gt;(string, Options), except
        /// without requiring a generic parameter.  For true dynamic deserialization, you 
        /// should use DeserializeDynamic instead.
        /// 
        /// Pass an Options object to specify the particulars (such as DateTime formats) of
        /// the JSON being deserialized.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// </summary>
        public static object Deserialize(string text, Type type, Options options = null)
        {
            if (text == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type == typeof(object))
            {
                return DeserializeDynamic(text, options);
            }

            return DeserializeIndirect.DeserializeFromString(text, type, options);
        }

        /// <summary>
        /// Deserializes JSON from the given TextReader.
        /// 
        /// Pass an Options object to specify the particulars (such as DateTime formats) of
        /// the JSON being deserialized.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// </summary>
        public static T Deserialize<T>(TextReader reader, Options options = null)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (typeof(T) == typeof(object))
            {
                return DeserializeDynamic(reader, options);
            }

            try
            {
                options = options ?? DefaultOptions;
                reader = reader.MakeSupportPeek();

                switch (options.UseDateTimeFormat)
                {
                    case DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<Deserialize.MicrosoftStyle, T>.Get()(reader, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<Deserialize.MicrosoftStyleCamelCase, T>.Get()(reader, 0);
                        }
                        break;
                    case DateTimeFormat.MillisecondsSinceUnixEpoch:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<MillisecondStyle, T>.Get()(reader, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<MillisecondStyleCamelCase, T>.Get()(reader, 0);
                        }
                        break;
                    case DateTimeFormat.SecondsSinceUnixEpoch:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<SecondStyle, T>.Get()(reader, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<SecondStyleCamelCase, T>.Get()(reader, 0);
                        }
                        break;
                    case DateTimeFormat.ISO8601:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<ISO8601Style, T>.Get()(reader, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<ISO8601StyleCamelCase, T>.Get()(reader, 0);
                        }
                        break;
                    case DateTimeFormat.RFC1123:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<RFC1123Style, T>.Get()(reader, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<RFC1123StyleCamelCase, T>.Get()(reader, 0);
                        }
                        break;
                }
                throw new InvalidOperationException("Unexpected Options: " + options);
            }
            catch (Exception e)
            {
                if (e is DeserializationException) throw;

                throw new DeserializationException(e, reader, false);
            }
        }

        /// <summary>
        /// Deserializes JSON from the given string.
        /// 
        /// Pass an Options object to specify the particulars (such as DateTime formats) of
        /// the JSON being deserialized.  If omitted Options.Default is used, unless JSON.SetDefaultOptions(Options) has been
        /// called with a different Options object.
        /// </summary>
        public static T Deserialize<T>(string text, Options options = null)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (typeof(T) == typeof(object))
            {
                return DeserializeDynamic(text, options);
            }

            try
            {
                options = options ?? DefaultOptions;

                var thunk = new ThunkReader(text);

                switch (options.UseDateTimeFormat)
                {
                    case DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<Deserialize.MicrosoftStyle, T>.GetFromString()(ref thunk, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<Deserialize.MicrosoftStyleCamelCase, T>.GetFromString()(ref thunk, 0);
                        }
                        break;
                    case DateTimeFormat.MillisecondsSinceUnixEpoch:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<MillisecondStyle, T>.GetFromString()(ref thunk, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<MillisecondStyleCamelCase, T>.GetFromString()(ref thunk, 0);
                        }
                        break;
                    case DateTimeFormat.SecondsSinceUnixEpoch:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<SecondStyle, T>.GetFromString()(ref thunk, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<SecondStyleCamelCase, T>.GetFromString()(ref thunk, 0);
                        }
                        break;
                    case DateTimeFormat.ISO8601:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<ISO8601Style, T>.GetFromString()(ref thunk, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<ISO8601StyleCamelCase, T>.GetFromString()(ref thunk, 0);
                        }
                        break;
                    case DateTimeFormat.RFC1123:
                        switch (options.SerializationNameFormat)
                        {
                            case SerializationNameFormat.Verbatim:
                                return JilFork.Deserialize.TypeCache<RFC1123Style, T>.GetFromString()(ref thunk, 0);
                            case SerializationNameFormat.CamelCase:
                                return JilFork.Deserialize.TypeCache<RFC1123StyleCamelCase, T>.GetFromString()(ref thunk, 0);
                        }
                        break;
                }
                throw new InvalidOperationException("Unexpected Options: " + options);
            }
            catch (Exception e)
            {
                if (e is DeserializationException) throw;

                throw new DeserializationException(e, false);
            }
        }

        /// <summary>
        /// Deserializes JSON from the given TextReader, inferring types from the structure of the JSON text.
        /// 
        /// For the best performance, use the strongly typed Deserialize method when possible.
        /// </summary>
        public static dynamic DeserializeDynamic(TextReader reader, Options options = null)
        {
            options = options ?? DefaultOptions;

            var built = DynamicDeserializer.Deserialize(reader.MakeSupportPeek(), options);

            return built.BeingBuilt;
        }

        /// <summary>
        /// Deserializes JSON from the given string, inferring types from the structure of the JSON text.
        /// 
        /// For the best performance, use the strongly typed Deserialize method when possible.
        /// </summary>
        public static dynamic DeserializeDynamic(string str, Options options = null)
        {
            using (var reader = new StringReader(str))
            {
                return DeserializeDynamic(reader, options);
            }
        }
    }
}
