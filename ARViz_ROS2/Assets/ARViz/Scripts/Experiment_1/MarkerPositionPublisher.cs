/*********************************************************************
 * Software License Agreement (BSD License)
 *
 *  Copyright (c) 2018, Intelligent Robotics Core S.L.
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *   * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *   * Redistributions in binary form must reproduce the above
 *     copyright notice, this list of conditions and the following
 *     disclaimer in the documentation and/or other materials provided
 *     with the distribution.
 *   * Neither the name of Intelligent Robotics Core nor the names of its
 *     contributors may be used to endorse or promote products derived
 *     from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 *  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 *  BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 *  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 *  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 *  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
 *  ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 *  POSSIBILITY OF SUCH DAMAGE.
 **********************************************************************/

/* Author: David Vargas Frutos - dvargasfr@gmail.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class MarkerPositionPublisher : MonoBehaviour
{
    INode node;
    TransformBroadcaster tfbr_;
    std_msgs.msg.String msg;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("marker_pos_publisher");
        tfbr_ = new TransformBroadcaster(ref node);

        // Invokes the method PublishMarker every second
        InvokeRepeating("PublishMarker", 0, 1f);
    }

    void PublishMarker()
    // void Update()
    {
        if (RCLdotnet.Ok())
        {
            Debug.Log("########## PublishMarker");

            geometry_msgs.msg.TransformStamped w2m = new geometry_msgs.msg.TransformStamped();

            w2m.Header.Frame_id = "world";

            System.Tuple<int, uint> ts = RCLdotnet.Now();

            w2m.Header.Stamp.Sec = ts.Item1;
            w2m.Header.Stamp.Nanosec = ts.Item2;
            w2m.Child_frame_id = "marker";

            // FRONT (red)
            w2m.Transform.Translation.X = 3;
            // LEFT (green)
            w2m.Transform.Translation.Y = 0;
            // UP (blue)
            w2m.Transform.Translation.Z = 0;
            w2m.Transform.Rotation.X = 0;
            w2m.Transform.Rotation.Y = 0;
            w2m.Transform.Rotation.Z = 0;
            w2m.Transform.Rotation.W = 1;

            tfbr_.SendTransform(ref w2m);
        }
    }
}

