using LabCDiezFacultativ.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace LabCDiezFacultativ.Repo
{
    public class XMLRepository<ID, E> : InMemoryRepository<ID, E> where E : Entity<ID> 
    {
        string Filename { get; }

        public XMLRepository(string filename)
        {
            Filename = filename;
            Load();
        }        

        public void Load()
        {
            if (!File.Exists(Filename)) return;
            XmlSerializer serializer = new XmlSerializer(typeof(List<E>));            
            using (FileStream stream = new FileStream(Filename, FileMode.Open))
            {
                foreach(E entity in serializer.Deserialize(stream) as List<E>)
                {
                    base.Add(entity);
                }
                stream.Close();
            }            
        }

        public override E Add(E e)
        {
            E entity = base.Add(e);
            Save();            
            return entity;
        }

        public override E RemoveById(ID id)
        {
            E entity = base.RemoveById(id);
            Save();
            return entity;
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<E>));                        
            using (FileStream stream = new FileStream(Filename, FileMode.Create)) 
            {
                serializer.Serialize(stream, GetAll());
                stream.Close();
            }                        
        }
    }
}
