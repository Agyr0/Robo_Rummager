using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agyr.Workshop
{
    public class WorkshopManager : Singleton<WorkshopManager>
    {
        [SerializeField]
        private WorkshopStorage workshopStorage;


        public WorkshopStorage WorkshopStorage
        {
            get { return workshopStorage; }
            set { workshopStorage = value; }
        }

    }
}