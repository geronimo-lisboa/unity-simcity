using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepository<Entity, Key> {
    Entity CreateNew();
    Entity FindById(Key id);
    Entity Save(Entity e);
    IEnumerable<Entity> FindAll();
    void Delete(Entity e);
    List<TerrainMK2> GetAll();
}
