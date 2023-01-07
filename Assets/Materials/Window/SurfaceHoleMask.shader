Shader "Unlit/SurfaceHoleMask"
{
   
        SubShader
        {

            Tags { "Queue" = "Geometry-1" }  // Write to the stencil buffer before drawing any geometry to the screen
           ColorMask 0 // Don't write to any colour channels
           ZWrite Off // Don't write to the Depth buffer

            Stencil
            {
                Ref 1
                Pass replace
            }
            Pass
            {

            }
        }
}