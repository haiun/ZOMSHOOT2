#ifndef ZOMBIE_STANDARD_CORE_FORWARD_INCLUDED
#define ZOMBIE_STANDARD_CORE_FORWARD_INCLUDED

#if defined(UNITY_NO_FULL_STANDARD_SHADER)
#   define UNITY_STANDARD_SIMPLE 1
#endif

#include "UnityStandardConfig.cginc"

#if UNITY_STANDARD_SIMPLE
    #include "ZombieStandardCoreForwardSimple.cginc"
    VertexOutputBaseSimple vertBase (VertexInput v) { return vertZombieForwardBaseSimple(v); }
    VertexOutputForwardAddSimple vertAdd (VertexInput v) { return vertZombieForwardAddSimple(v); }
    half4 fragBase (VertexOutputBaseSimple i) : SV_Target { return fragZombieForwardBaseSimpleInternal(i); }
    half4 fragAdd (VertexOutputForwardAddSimple i) : SV_Target { return fragZombieForwardAddSimpleInternal(i); }
#else
    #include "ZombieStandardCore.cginc"
    VertexOutputForwardBase vertBase (VertexInput v) { return vertZombieForwardBase(v); }
    VertexOutputForwardAdd vertAdd (VertexInput v) { return vertZombieForwardAdd(v); }
    half4 fragBase (VertexOutputForwardBase i) : SV_Target { return fragZombieForwardBaseInternal(i); }
    half4 fragAdd (VertexOutputForwardAdd i) : SV_Target { return fragZombieForwardAddInternal(i); }
#endif

#endif // ZOMBIE_STANDARD_CORE_FORWARD_INCLUDED
