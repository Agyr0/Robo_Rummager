using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agyr.Workshop
{
    public class WorkshopManager : Singleton<WorkshopManager>
    {
        [SerializeField]
        private WorkshopStorage workshopStorage;
        private WorkshopBench workshopBench;

        public WorkshopStorage WorkshopStorage
        {
            get { return workshopStorage; }
            set { workshopStorage = value; }
        }

        public WorkshopBench WorkshopBench
        {
            get { return workshopBench; }
            set { workshopBench = value; }
        }

    }
}