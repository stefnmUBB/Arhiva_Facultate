import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router';
import GameComponent from './GameComponent';
import { getLogger } from '../core';
import { GameContext } from './GameProvider';
import { IonContent, 
         IonHeader, 
         IonList, 
         IonLoading, 
         IonPage, 
         IonTitle, 
         IonToolbar,
         IonToast, 
         IonFab,
         IonFabButton,
         IonIcon,
         IonButton, 
         IonButtons,
         IonInfiniteScroll,
         IonInfiniteScrollContent,
         IonSearchbar,
         IonSelect, IonSelectOption } from '@ionic/react';

import { add } from 'ionicons/icons';
import { AuthContext } from '../auth';
import { NetworkState } from '../pages/NetworkState';
import { GameProps } from './GameProps';
import styles from "./styles.module.css";

const log = getLogger('GamesList');
const gamesPerPage = 3;
const filterValues = ["IsOpenSource", "IsClosedSource"];

export const GamesList: React.FC<RouteComponentProps> = ({ history }) => {
  const { games, fetching, fetchingError, successMessage, closeShowSuccess } = useContext(GameContext);
  const { logout } = useContext(AuthContext);
  const [isOpen, setIsOpen]= useState(false);
  const [index, setIndex] = useState<number>(0);
  const [gamesAux, setGamesAux] = useState<GameProps[] | undefined>([]);
  const [more, setHasMore] = useState(true);
  const [searchText, setSearchText] = useState('');
  const [filter, setFilter] = useState<string | undefined>(undefined);
  //const [hasFetched, setHasFetched] = useState(false);

  useEffect(()=>{
    if(fetching) setIsOpen(true);
    else setIsOpen(false);
  }, [fetching]);

  log('render');
  console.log(games);

  function handleLogout(){
    logout?.();
    history.push('/login');
  }

  //pagination
  useEffect(()=>{
    fetchData();
  }, [games]);

  // searching
  useEffect(()=>{
    if (searchText === "") {
      setGamesAux(games);
    }
    if (games && searchText !== "") {
      setGamesAux(games.filter(game => game.authors!.some((a)=>a.toLowerCase().includes(searchText.toLowerCase()))));
    }
  }, [searchText]);

   // filtering
   useEffect(() => {
    if (games && filter) {
        setGamesAux(games.filter(game => {
            if (filter === "IsOpenSource")
                return game.isOpenSource === true;
            else
                return game.isOpenSource === false;
        }));
    }
}, [filter]);

  function fetchData() {
    if(games){
      const newIndex = Math.min(index + gamesPerPage, games.length);
      if( newIndex >= games.length){
          setHasMore(false);
      }
      else{
          setHasMore(true);
      }
      setGamesAux(games.slice(0, newIndex));
      setIndex(newIndex);
    }
  }

  async function searchNext($event: CustomEvent<void>){
    await fetchData();
    await ($event.target as HTMLIonInfiniteScrollElement).complete();
  }

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>Homebrew corner</IonTitle>
          <IonSelect 
            className={styles.selectBar} 
            slot="end" 
            value={filter} 
            placeholder="Filter" 
            onIonChange={(e) => setFilter(e.detail.value)}>
                        {filterValues.map((each) => (
                            <IonSelectOption key={each} value={each}>
                                {each}
                            </IonSelectOption>
                        ))}
          </IonSelect>
          <NetworkState />
          <IonSearchbar className={styles.customSearchBar} placeholder="Search by developer" value={searchText} debounce={200} onIonInput={(e) => {
                        setSearchText(e.detail.value!);
                    }} slot="secondary">
          </IonSearchbar>
          <IonButtons slot='end'>
             <IonButton onClick={handleLogout}>Logout</IonButton>
          </IonButtons>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonLoading isOpen={isOpen} message="Fetching songs..." />
        {gamesAux && (
          <IonList>
            {gamesAux.map(game => 
              <GameComponent 
                key={game._id} 
                _id={game._id} 
                title={game.title} 
                onEdit={id => history.push(`/game/${id}`)}
                launchDate={game.launchDate} 
                platform={game.platform} 
                lastVersion={game.lastVersion} 
                url={game.url} 
                isOpenSource={game.isOpenSource}
                isNotSaved={game.isNotSaved}
                authors = {game.authors} totalReleases={game.totalReleases}/>           
            )}
          </IonList>
        )}
        <IonInfiniteScroll threshold="100px" disabled={!more} onIonInfinite={(e:CustomEvent<void>) => searchNext(e)} >
          <IonInfiniteScrollContent loadingText="Loading more food..." >
          </IonInfiniteScrollContent>
        </IonInfiniteScroll>
        {fetchingError && (
          <div>{fetchingError.message || 'Failed to fetch games'}</div>
        )}
        <IonFab vertical="bottom" horizontal="end" slot="fixed">
          <IonFabButton onClick={() => history.push('/game')}>
            <IonIcon icon={add} />
          </IonFabButton>
        </IonFab>
        <IonToast
          isOpen={!!successMessage}
          message={successMessage}
          position='bottom'
          buttons={[
            {
              text: 'Dismiss',
              role: 'cancel',
              handler: () => {
                console.log('More Info clicked');
              },
            }]}
          onDidDismiss={closeShowSuccess}
          duration={5000}
          />
      </IonContent>
    </IonPage>
  );
};