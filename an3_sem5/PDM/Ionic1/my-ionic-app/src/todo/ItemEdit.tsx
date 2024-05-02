import React, { useCallback, useContext, useEffect, useState } from 'react';
import {
  IonButton,
  IonButtons,
  IonCheckbox,
  IonContent,  
  IonDatetime,
  IonHeader,
  IonInput,
  IonItemOption,
  IonLabel,
  IonLoading,
  IonPage,  
  IonSelect,  
  IonSelectOption,  
  IonTitle,
  IonToolbar
} from '@ionic/react';
import { getLogger } from '../core';
import { ItemContext } from './ItemProvider';
import { RouteComponentProps } from 'react-router';
import { ItemProps } from './ItemProps';

const log = getLogger('ItemEdit');

interface ItemEditProps extends RouteComponentProps<{
  id?: string;
}> {}

const ItemEdit: React.FC<ItemEditProps> = ({ history, match }) => {
  const { items, saving, deleting, savingError, deletingError, saveItem, deleteItem2 } = useContext(ItemContext);
  const [title, setTitle] = useState('');
  const [launchDate, setLaunchDate] = useState<Date>(new Date(Date.now()));
  const [platform, setPlatform] = useState('');
  const [lastVersion, setLastVersion] = useState('');
  const [url, setUrl] = useState('');
  const [totalReleases, setTotalReleases]=useState(0);
  const [authors, setAuthors]=useState<string[]>([]);
  const [isOpenSource, setIsOpenSource]=useState<boolean>(false);

  const [item, setItem] = useState<ItemProps>();
  useEffect(() => {
    log('useEffect');
    const routeId = match.params.id || '';
    const item = items?.find(it => it.id === routeId);
    setItem(item);
    if (item) {
      console.log(item);
      setTitle(item.title);                              
      setLaunchDate(item.launchDate)      
      setLastVersion(item.lastVersion)
      setPlatform(item.platform)
      setUrl(item.url)    
      setTotalReleases(item.totalReleases)
      setAuthors(item.authors);
      setIsOpenSource(item.isOpenSource);      
    }
  }, [match.params.id, items]);

  const handleSave = useCallback(() => {    
    const editedItem = { ...item, title, launchDate, platform, lastVersion, url, totalReleases, authors, isOpenSource };
    saveItem && saveItem(editedItem).then(() => history.goBack());
  }, [item, saveItem, title, launchDate, platform, url, lastVersion, totalReleases, authors, isOpenSource, history]);

  const handleDelete = useCallback(() => {
    const editedItem = item;
    console.log(deleteItem2);
    console.log(editedItem);
    deleteItem2 && deleteItem2(editedItem).then(() => history.goBack());
  }, [item, deleteItem2, history]);    

  log('render');
  console.log(item);
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>Edit</IonTitle>
          <IonButtons slot="end">
            <IonButton onClick={handleSave}> Save </IonButton>
            <IonButton onClick={handleDelete}> Delete </IonButton>
          </IonButtons>
        </IonToolbar>
      </IonHeader>
      <IonContent>        
        <br/>
        <IonLabel><b>Title</b></IonLabel>
        <IonInput value={title} onIonChange={e => setTitle(e.detail.value || '')} />
        <br/>
        <IonLabel><b>Authors</b> (separated by ",")</IonLabel>
        <IonInput value={authors.join(",")} onIonChange={e => setAuthors((e.detail.value || '').split(",").filter(x=>x!=""))} />
        <br/>
        <IonLabel><b>Launch Date</b></IonLabel>
        <IonDatetime presentation="date" value={launchDate.toString()} onIonChange={e=>{ setLaunchDate(new Date(Date.parse(e.detail.value?.toString() || new Date(Date.now()).toString())))}}/>
        <br/>
        <IonLabel><b>Platform</b></IonLabel>
        <IonSelect value={platform} onIonChange={e => setPlatform(e.detail.value || '')}>
          <IonSelectOption>Game Boy</IonSelectOption>          
          <IonSelectOption>Game Boy Color</IonSelectOption>
          <IonSelectOption>Game Boy Advance</IonSelectOption>
          <IonSelectOption>Nintendo DS</IonSelectOption>
          <IonSelectOption>Nintendo 3DS</IonSelectOption>
        </IonSelect> 
        <br/>
        <IonLabel><b>Last Version</b></IonLabel>
        <IonInput value={lastVersion} onIonChange={e => setLastVersion(e.detail.value || '')} />

        <br/>
        <IonLabel><b>Open Source</b></IonLabel>
        <IonCheckbox checked={isOpenSource} onIonChange={e =>{ setIsOpenSource(e.detail.checked) } } />        

        <br/>
        <IonLabel><b>Total Releases</b></IonLabel>
        <IonInput type="number" value={totalReleases} onIonChange={e => setTotalReleases(Number.parseInt(e.detail.value || "0"))} />

        <br/>
        <IonLabel><b>External link</b></IonLabel>
        <IonInput value={url} onIonChange={e => setUrl(e.detail.value || '')} />              

        <IonLoading isOpen={saving} />
        {savingError && (
          <div>{savingError.message || 'Failed to save item'}</div>
        )}
         <IonLoading isOpen={deleting} />
        {deletingError && (
          <div>{deletingError.message || 'Failed to delete item'}</div>
        )}
      </IonContent>
    </IonPage>
  );
};

export default ItemEdit;
