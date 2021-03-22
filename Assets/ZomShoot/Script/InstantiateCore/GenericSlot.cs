using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class GenericSlot<TData, TSlot> : MonoBehaviour where TData : class where TSlot : GenericSlot<TData, TSlot>
{
    public class Grid
    {
        private List<TSlot> slotList = new List<TSlot>();
        public List<TSlot> SlotList { get { return slotList; } }

        public Transform ParentTransform { get; private set; }

        private Func<List<TData>, List<TSlot>> onCraete = null;
        private Action<TSlot> onDestroy = null;

        public Grid(Transform parent, Func<List<TData>, List<TSlot>> onCraete, Action<TSlot> onDestroy)
        {
            ParentTransform = parent;
            this.onCraete = onCraete;
            this.onDestroy = onDestroy;
        }

        public Grid(Transform parent)
        {
            ParentTransform = parent;
            onCraete = CreateSlotDefault;
            onDestroy = DestroySlotDefault;
        }

        private List<TSlot> CreateSlotDefault(List<TData> dataList)
        {
            var slotList = GenericPrefab.Instantiate<TSlot>(ParentTransform, dataList.Count);
            for (int i = 0; i < dataList.Count; ++i)
            {
                slotList[i].SetData(dataList[i]);
            }
            return slotList;
        }

        private void DestroySlotDefault(TSlot slot)
        {
            GameObject.Destroy(slot.gameObject);
        }

        public void ApplyList(List<TData> dataList)
        {
            int modelCount = dataList.Count;
            int removeCount = SlotList.Count - modelCount;
            if (removeCount > 0)
            {
                for (int i = modelCount; i < SlotList.Count; ++i)
                {
                    onDestroy(SlotList[i]);
                }
                SlotList.RemoveRange(modelCount, removeCount);
            }

            for (int i = 0; i < SlotList.Count; ++i)
            {
                SlotList[i].SetData(dataList[i]);
            }

            if (removeCount < 0)
            {
                var newDataList = dataList.GetRange(SlotList.Count, -removeCount);
                SlotList.AddRange(onCraete(newDataList));
            }
        }
    }

    public TData Data { get; private set; }

    public void SetData(TData data, bool compRef = true)
    {
        if (compRef && Data == data)
            return;

        Data = data;
        OnSetData(data);
    }

    public void Invalidate()
    {
        OnSetData(Data);
    }

    protected abstract void OnSetData(TData data);
}
