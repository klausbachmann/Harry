using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace burglar
{
    public class BurglarAction
    {

        public Actions mAction;
        public Vector3 mMovePosition;
        public int mWaitSeconds;

        public BurglarAction(Actions mAction, Vector3 mMovePosition)
        {
            this.mAction = mAction;
            this.mMovePosition = mMovePosition;
        }

        public BurglarAction(Actions mAction, int mWaitSeconds)
        {
            this.mAction = mAction;
            this.mWaitSeconds = mWaitSeconds;
        }





    }
}