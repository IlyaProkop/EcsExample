using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Leopotam.Ecs;
using Client;

public class ObjectStackSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private GameData _gameData;

    private EcsFilter<ObjectStackProvider> _stackFilter;
    private EcsFilter<GoToStackObject> _goToStackObjectsFilter;
    private EcsFilter<GoFromStackObject> _goFromStackObjectsFilter;

    public void Init()
    {
        foreach (var idx in _stackFilter)
        {

            _stackFilter.Get1(idx).Objects = new List<EcsEntity>();
            ref EcsEntity stackEntity = ref _stackFilter.GetEntity(idx);
            CreateStackGrid(ref stackEntity);
        }
    }

    public void Run()
    {
        foreach (var idx in _goToStackObjectsFilter)
        {
            ref EcsEntity entity = ref _goToStackObjectsFilter.GetEntity(idx);
            ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
            ref ObjectCurrentStack entityStack = ref entity.Get<ObjectCurrentStack>();
            entity.Get<StackObject>();

            entityStack.Stack.Objects.Add(entity);
            entity.Get<VelocityMoving>().Target = entityStack.Stack.GridPoints[entityStack.Stack.Objects.Count].position;
            entity.Get<VelocityMoving>().Speed = 1;
            entity.Get<VelocityMoving>().Accuracy = 1;

            //entityGo.Value.transform.DOScale(Vector3.one, 0.1f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutCubic); //del
            //entityGo.Value.transform.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.OutCubic); //del

            entity.Del<GoToStackObject>();
        }

        foreach (var idx in _goFromStackObjectsFilter)
        {
            ref EcsEntity entity = ref _goFromStackObjectsFilter.GetEntity(idx);
            ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
            ref ObjectCurrentStack entityStack = ref entity.Get<ObjectCurrentStack>();
            ref GoFromStackObject goFromStack = ref entity.Get<GoFromStackObject>();
            entity.Get<StackObject>();

            entityStack.Stack.Objects.Remove(entity);
            entity.Get<VelocityMoving>().Target = goFromStack.To.position;
            entity.Get<VelocityMoving>().Speed = 5;
            entity.Get<VelocityMoving>().Accuracy = 0.1f;

            //entityGo.Value.transform.DOScale(Vector3.one, 0.1f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutCubic); //del
            //entityGo.Value.transform.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.OutCubic); //del

            entity.Del<GoFromStackObject>();
        }
    }

    private void CreateStackGrid(ref EcsEntity entity)
    {
        ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
        ref ObjectStackProvider stack = ref entity.Get<ObjectStackProvider>();
        stack.GridPoints = new List<Transform>();
        Vector3 parentPos = entityGo.Value.transform.position;

        for (int z = 0; z < stack.Rows; z++)
        {
            for (int x = 0; x < stack.Columns; x++)
            {
                for (int y = 0; y < stack.ObjectsInColumn; y++)
                {
                    Vector3 _pos = parentPos + new Vector3(x * stack.ObjectsOffset.x, y * stack.ObjectsOffset.y, z * stack.ObjectsOffset.z);
                    GameObject go = GameObject.Instantiate(_gameData.StaticData.EmptyPrefab, _pos, Quaternion.identity, entityGo.Value.transform);
                    stack.GridPoints.Add(go.transform);
                }
            }
        }
        stack.Capacity = stack.Rows * stack.Columns * stack.ObjectsInColumn;
    }

    //private IEnumerator GiveMoneyToCharacter()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    if (!IsCanGive()) yield break;
    //    StackableItem lastItem = GetLastItem();
    //    RemoveLastItem();
    //    if (!lastItem) yield break;
    //    lastItem.transform.parent = null;
    //    lastItem.PickUp(characterAtCell.moneyStack);

    //    giveMoneyCounter--;
    //    if (giveMoneyCounter > 0)
    //        StartCoroutine(GiveMoneyToCharacter());
    //    else
    //        isRunning = false;
    //}

    //private void TryTakeMoneyFromCharacter(int _howMuch)
    //{
    //    takeMoneyCounter = _howMuch;
    //    StartCoroutine(TakeMoneyFromCharacter());
    //}

    //private IEnumerator TakeMoneyFromCharacter()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    if (!IsCanTake()) yield break;
    //    var _lastItem = characterAtCell.moneyStack.GetLastItem();
    //    characterAtCell.moneyStack.RemoveLastItem();
    //    if (!_lastItem) yield break;
    //    _lastItem.transform.parent = null;
    //    _lastItem.PickUp(this);

    //    takeMoneyCounter--;
    //    if (takeMoneyCounter > 0)
    //        StartCoroutine(TakeMoneyFromCharacter());
    //}

    //private bool IsCanGive()
    //{
    //    return characterAtCell.moneyStack.HasEmptySpace() && GetItemsCount() + 1 > 0;
    //}

    //private bool IsCanTake()
    //{
    //    return characterAtCell.moneyStack.GetItemsCount() + 1 > 0;
    //}

    //public void GiveMoneyToCharacterAtCell(Character _character, int _howMuch)
    //{
    //    characterAtCell = _character;
    //    TryGiveMoneyToCharacter(_howMuch);
    //}

    //public void TakeMoneyFromCharacterAtCell(Character _character, int _howMuch)
    //{
    //    characterAtCell = _character;
    //    TryTakeMoneyFromCharacter(_howMuch);
    //}

    //public StackableItem GetLastItem()
    //{
    //    return stackedItems[GetItemsCount()];
    //}

    //private int GetItemsCount()
    //{
    //    return stackedItems.Count - 1;
    //}

    //public void ShowItems(bool _isShow)
    //{
    //    if (_isShow)
    //        foreach (var item in stackedItems)
    //            item.gameObject.SetActive(false);
    //    else
    //        foreach (var item in stackedItems)
    //            item.gameObject.SetActive(true);
    //}

    //public void RemoveLastItem()
    //{
    //    if (stackedItems.Count > 0 && GetLastItem())
    //        stackedItems.Remove(GetLastItem());
    //}

    //public bool HasEmptySpace()
    //{
    //    return stackedItems.Count < stackLimit;
    //}

}