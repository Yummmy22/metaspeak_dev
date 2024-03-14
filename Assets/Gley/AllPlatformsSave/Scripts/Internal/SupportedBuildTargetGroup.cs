namespace Gley.AllPlatformsSave.Internal
{
    public enum SupportedBuildTargetGroup
    {
        //
        // Summary:
        //     Mac/PC standalone target.
        Standalone = 1,
        //
        // Summary:
        //     Apple iOS target.
        iOS = 4,
        //
        // Summary:
        //     Android target.
        Android = 7,
        //
        // Summary:
        //     WebGL.
        WebGL = 13,
        //
        // Summary:
        //     Windows Store Apps target.
        WSA = 14,
        //
        //
        // Summary:
        //     Sony Playstation 4 target.
        PS4 = 19,
        //
        // Summary:
        //     Microsoft Xbox One target.
        XboxOne = 21,
        //
#if UNITY_5_4_OR_NEWER
        //
        // Summary:
        //     Apple's tvOS target.
        tvOS = 25,
#endif
    }
}