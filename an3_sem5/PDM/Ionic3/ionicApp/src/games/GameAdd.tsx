import React, { useCallback, useContext, useEffect, useState } from 'react';
import {
  IonButton,
  IonButtons,
  IonContent,
  IonHeader,
  IonInput,
  IonLoading,
  IonPage,
  IonTitle,
  IonToolbar,
  IonBackButton,
  IonLabel,
  IonDatetime,
  IonSelect,
  IonSelectOption,
  IonCheckbox
} from '@ionic/react';
import { getLogger } from '../core';
import { RouteComponentProps } from 'react-router';
import { GameContext } from './GameProvider';
import { GameProps } from './GameProps';
import styles from './styles.module.css';

const log = getLogger('SaveLogger');

interface GameEditProps extends RouteComponentProps<{
  id?: string;
}> {}

export const GameAdd: React.FC<GameEditProps> = ({ history, match }) => {
  log("game add here");
  const { games, updating, updateError, addGame } = useContext(GameContext);
  const [title, setTitle] = useState('');
  const [launchDate, setLaunchDate] = useState(new Date());
  const [platform, setPlatform] = useState('');
  const [lastVersion, setLastVersion] = useState("");
  const [url, setUrl] = useState("");
  const [authors, setAuthors] = useState<string[]>([]);
  const [totalReleases, setTotalReleases] = useState(0);
  const [isOpenSource, setIsOpenSource] = useState(false);

  
  const [gameToUpdate, setGameToUpdate] = useState<GameProps>();

  const handleAdd = useCallback(() => {
    const editedGame ={ ...gameToUpdate, title, launchDate, platform, lastVersion, url, authors, totalReleases, isOpenSource };
    //console.log(duration);
    //console.log(editedGame);
    log(editedGame);
    console.log(updateError);
    addGame && addGame(editedGame).then(() => history.goBack());
  }, [gameToUpdate, addGame, title, launchDate, platform, lastVersion, url, authors, totalReleases, isOpenSource , history]);  

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
        <IonButtons slot="start">
            <IonBackButton></IonBackButton>
          </IonButtons>
          <IonTitle>Edit</IonTitle>
          <IonButtons slot="end">
            <IonButton onClick={handleAdd}>
              Add
            </IonButton>
          </IonButtons>
        </IonToolbar>
      </IonHeader>
      {/*<IonContent>
        <IonInput label="Title:" className={styles.customInput} placeholder="New Title" value={title} onIonInput={e => setTitle(prev => e.detail.value || '')} />
        <IonInput label="Artist:" className={styles.customInput} placeholder="New Artist" value={artist} onIonInput={e => setArtist(prev => e.detail.value || '')} />
        <IonInput label="Duration:" className={styles.customInput} placeholder="New duration" value={duration} onIonInput={e => e.detail.value ? setDuration(prev => e.detail.value!) : setDuration('') }/>
        <IonInput label="DateOfRelease:" className={styles.customInput} placeholder="Choose date" value={new Date(date).toDateString()} />
        <IonDatetime
                onIonChange={(e) => dateChanged(e.detail.value)}>
        </IonDatetime>
        <IonInput label="Featured Artists:" className={styles.customInput} placeholder="True/False" value={option==true ? 'True' : 'False'} />
        <IonSelect value={option} onIonChange={e => setOption(e.detail.value)}>
          <IonSelectOption value={true}>
            {'True'}
          </IonSelectOption>
          <IonSelectOption value={false}>
            {'False'}
          </IonSelectOption>
        </IonSelect>
        <IonLoading isOpen={updating} />
        {updateError && (
          <div className={styles.errorMessage}>{updateError.message || 'Failed to save item'}</div>
        )}
        </IonContent>*/}
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

        <IonLoading isOpen={updating} />
        {updateError && (
          <div className={styles.errorMessage}>{updateError.message || 'Failed to update item'}</div>
        )}
      </IonContent>
    </IonPage>
  );
}
