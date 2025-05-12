using System.Collections.Generic;
using System;
using UnityEngine.Events;

using EntityID = System.Int32;

class EntityManagerLogger : Logger
{

}

class EntityManager
{
    public UnityEvent<IGameEntity> OnEntityAddedCallback;
    public UnityEvent<Type> OnEntityDestroyedCallback;
    List<IGameEntity> m_loadedEntities = new();
    /*List<EntityID> m_loadedEntityIDs = new();*/
    /*int persistentDataOffset = 0;*/
    List<bool> m_loadedEntitiesActiveState = new();
    /*EntityID s_lastID = 0;*/

    static EntityManager s_entityManager;

    public static EntityManager Get()
    {
        if(s_entityManager == null)
        {
            s_entityManager = new();
        }

        return s_entityManager;
    }

    public EntityID CreatePersistentEntity<TEntity>(out TEntity outEntity, params object[] variadicArgs) where TEntity : IGameEntity
    {
        int entityID = m_loadedEntities.FindIndex((IGameEntity entity) => { return entity == null;});

        if(entityID == -1)
        {
            outEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);
            m_loadedEntities.Add(outEntity);
            m_loadedEntitiesActiveState.Add(true);
            return m_loadedEntities.Count - 1;
        }
        else
        {
            outEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);
            m_loadedEntities[entityID] = outEntity;
            m_loadedEntitiesActiveState[entityID] = true;
            return entityID;
        }
    }

    /*public EntityID CreatePersistentEntity<TEntity>(out TEntity outEntity, params object[] variadicArgs) where TEntity : IGameEntity*/
    /*{*/
    /*    int deletedEntityID = -1;*/
    /**/
    /*    for(int i = 0; i < persistentDataOffset; i++)*/
    /*    {*/
    /*        if(m_loadedEntitiesActiveState[i])*/
    /*        {*/
    /*            deletedEntityID = i;*/
    /*            break;*/
    /*        }*/
    /*    }*/
    /**/
    /*    persistentDataOffset++;*/
    /*    if(deletedEntityID == -1)*/
    /*    {*/
    /*        outEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);*/
    /*        m_loadedEntities.Insert(persistentDataOffset, outEntity);*/
    /*        m_loadedEntityIDs.Insert(persistentDataOffset, s_lastID++);*/
    /*        m_loadedEntitiesActiveState.Insert(persistentDataOffset, true);*/
    /*        return persistentDataOffset;*/
    /*    }*/
    /*    else*/
    /*    {*/
    /*        return CreateEntityWithID<TEntity>(deletedEntityID, out outEntity);*/
    /*    }*/
    /*}*/

    //useless
    /*private EntityID CreateEntity<TEntity>(out TEntity outEntity, params object[] variadicArgs) where TEntity : IGameEntity*/
    /*{*/
    /*    int deletedEntityIndex = m_loadedEntitiesActiveState.FindIndex((bool state) => { return state == false;});*/
    /*    for(int i=0; i < m_loadedEntitiesActiveState.Count; i++)*/
    /*    {*/
    /*        if(i >= persistentDataOffset && m_loadedEntitiesActiveState[i])*/
    /*        {*/
    /*            deletedEntityIndex = i;*/
    /*            break;*/
    /*        }*/
    /*    }*/
    /**/
    /*    return CreateEntityWithID<TEntity>(deletedEntityIndex, out outEntity, variadicArgs);*/
    /*}*/
    /**/
    /*private EntityID CreateEntityWithID<TEntity>(int entityID, out TEntity outEntity, params object[] variadicArgs) where TEntity : IGameEntity*/
    /*{*/
    /*    if(entityID == -1)*/
    /*    {*/
    /*        outEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);*/
    /*        m_loadedEntities.Add(outEntity);*/
    /*        m_loadedEntityIDs.Add(s_lastID++);*/
    /*        m_loadedEntitiesActiveState.Add(true);*/
    /*        entityID = m_loadedEntityIDs[m_loadedEntityIDs.Count - 1];*/
    /*    }*/
    /*    else*/
    /*    {*/
    /*        outEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), args:variadicArgs);*/
    /*        m_loadedEntities[entityID] = outEntity; //m_loadedEntityIDs is the same*/
    /*        m_loadedEntitiesActiveState[entityID] = true;*/
    /*        entityID = m_loadedEntityIDs[entityID];*/
    /*    }*/
    /**/
    /*    if(OnEntityAddedCallback != null)*/
    /*    {*/
    /*        OnEntityAddedCallback.Invoke(m_loadedEntities[entityID]);*/
    /*    }*/
    /**/
    /*    return entityID;*/
    /*}*/
    /**/
    public bool Exists<TEntity>(TEntity entityToCheck) where TEntity : IGameEntity
    {
        return m_loadedEntities.FindIndex((IGameEntity entity) => { return entity == (IGameEntity)entityToCheck;}) != -1;
    }

    public bool Exists(EntityID id)
    {
        return m_loadedEntitiesActiveState.Count > id && m_loadedEntitiesActiveState[id];
    }
    /**/

    public void DestroyPersistentEntity<TEntity>(TEntity entityToCheck) where TEntity : IGameEntity
    {
        DestroyPersistentEntity(GetEntity(entityToCheck));
    }
    public void DestroyPersistentEntity(EntityID id)
    {
        if(Exists(id))
        {
            OnEntityDestroyedCallback.Invoke(m_loadedEntities[id].GetType());
            m_loadedEntities[id] = null;
            m_loadedEntitiesActiveState[id] = false;
        }
    }

    public EntityID GetEntity<TEntity>(TEntity entityToGet) where TEntity : IGameEntity
    {
        return m_loadedEntities.FindIndex((IGameEntity entity) => { return entity == (IGameEntity)entityToGet;});
    }
    public TEntity GetEntity<TEntity>(EntityID id) where TEntity : IGameEntity
    {
        if(Exists(id))
        {
            return (TEntity)m_loadedEntities[id];
        }

        return default;
    }
}
