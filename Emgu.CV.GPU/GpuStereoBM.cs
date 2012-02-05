//----------------------------------------------------------------------------
//  Copyright (C) 2004-2012 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

﻿using System;
using System.Runtime.InteropServices;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Emgu.CV.GPU
{
   /// <summary>
   /// Use Block Matching algorithm to find stereo correspondence
   /// </summary>
   public class GpuStereoBM : UnmanagedObject
   {
      /// <summary>
      /// Preset type
      /// </summary>
      public enum PresetType
      {
         /// <summary>
         /// Basic
         /// </summary>
         BasicPreset = 0,
         /// <summary>
         /// prefilter xsobel
         /// </summary>
         PrefilterXSobel = 1
      };

      /// <summary>
      /// Create a stereoBM 
      /// </summary>
      /// <param name="preset">Preset type</param>
      /// <param name="numberOfDisparities">The number of disparities. Must be multiple of 8. Use 64 for default </param>
      /// <param name="winSize">The SAD window size. Use 19 for default</param>
      public GpuStereoBM(PresetType preset, int numberOfDisparities, int winSize)
      {
         _ptr = GpuInvoke.GpuStereoBMCreate(preset, numberOfDisparities, winSize);
      }

      /// <summary>
      /// Computes disparity map for the input rectified stereo pair.
      /// </summary>
      /// <param name="left">The left single-channel, 8-bit image</param>
      /// <param name="right">The right image of the same size and the same type</param>
      /// <param name="disparity">The disparity map</param>
      /// <param name="stream">Use a Stream to call the function asynchronously (non-blocking) or null to call the function synchronously (blocking).</param>
      public void FindStereoCorrespondence(GpuImage<Gray, Byte> left, GpuImage<Gray, Byte> right, GpuImage<Gray, Byte> disparity, Stream stream)
      {
         GpuInvoke.GpuStereoBMFindStereoCorrespondence(_ptr, left, right, disparity, stream);
      }

      /// <summary>
      /// Release the stereo state and all the memory associate with it
      /// </summary>
      protected override void DisposeObject()
      {
         GpuInvoke.GpuStereoBMRelease(ref _ptr);
      }
   }

   public static partial class GpuInvoke
   {
      [DllImport(CvInvoke.EXTERN_GPU_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal static extern IntPtr GpuStereoBMCreate(GpuStereoBM.PresetType preset, int ndisparities, int winSize);

      [DllImport(CvInvoke.EXTERN_GPU_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal static extern void GpuStereoBMFindStereoCorrespondence(IntPtr stereoBM, IntPtr left, IntPtr right, IntPtr disparity, IntPtr stream);

      [DllImport(CvInvoke.EXTERN_GPU_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal static extern void GpuStereoBMRelease(ref IntPtr stereoBM);
   }
}