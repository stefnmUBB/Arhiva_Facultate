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

const log = getLogger('EditLogger');

interface GameEditProps extends RouteComponentProps<{
  id?: string;
}> {}

export const GameEdit: React.FC<GameEditProps> = ({ history, match }) => {      
  log("game edit here");
  const { games, updating, updateError, updateGame, deleteGame } = useContext(GameContext);
  const [title, setTitle] = useState('');
  const [launchDate, setLaunchDate] = useState<Date>(new Date(Date.now()));
  const [platform, setPlatform] = useState('');
  const [lastVersion, setLastVersion] = useState("");
  const [url, setUrl] = useState("");
  const [authors, setAuthors] = useState<string[]>([]);
  const [totalReleases, setTotalReleases] = useState(0);
  const [isOpenSource, setIsOpenSource] = useState(false);
  const [gameToUpdate, setGameToUpdate] = useState<GameProps>();

  useEffect(() => {
    const routeId = match.params.id || '';
    console.log("GameEdit use effect");
    console.log(routeId);
    //const idNumber = parseInt(routeId);
    const game = games?.find(it => it._id === routeId);
    setGameToUpdate(game);
    if(game){
      setTitle(game.title);   
      setAuthors(game.authors);
      setLaunchDate(game.launchDate);
      setIsOpenSource(game.isOpenSource);
      setLastVersion(game.lastVersion);
      setPlatform(game.platform);
      setUrl(game.url);   
    }
  }, [match.params.id, games]);

  const handleUpdate = useCallback(() => {
    const editedGame ={ ...gameToUpdate, title, launchDate, platform, lastVersion, url, authors, totalReleases, isOpenSource };
    //console.log(duration);
    //console.log(editedSong);
    log(editedGame);
    console.log(updateGame);
    updateGame && updateGame(editedGame).then(() => history.goBack());
  }, [gameToUpdate, updateGame, title, launchDate, platform, lastVersion, url, authors, totalReleases, isOpenSource, history]);

  log("render GameEdit")
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
        <IonButtons slot="start">
            <IonBackButton></IonBackButton>
          </IonButtons>
          <IonTitle>Edit</IonTitle>
          <IonButtons slot="end">
            <IonButton onClick={handleUpdate}>
              Update
            </IonButton>            
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
        <IonDatetime presentation="date" value={new Date(launchDate).toISOString()} onIonChange={e=>{ setLaunchDate(new Date(Date.parse(e.detail.value?.toString() || new Date(Date.now()).toString())))}}/>
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
