import React, { useCallback, useEffect, useReducer, useContext } from 'react';
import PropTypes from 'prop-types';
import { getLogger } from '../core';
import { getAllGames, updateGameAPI, createGameAPI, newWebSocket, deleteGameAPI } from './GameApi';
import { GameProps } from './GameProps';
import { AuthContext } from '../auth';
import { useNetwork } from '../pages/useNetwork';
import {useIonToast} from "@ionic/react";
import { Preferences } from '@capacitor/preferences';

const log = getLogger('GameProvider');

type UpdateGameFn = (game: GameProps) => Promise<any>;
type DeleteGameFn = (id: string) => Promise<any>;

interface GamesState {
    games?: GameProps[];
    fetching: boolean;
    fetchingError?: Error | null;
    updating: boolean,
    updateError?: Error | null,
    updateGame?: UpdateGameFn,
    addGame?: UpdateGameFn,
    deleteGame?: DeleteGameFn;
    successMessage?: string;
    closeShowSuccess?: () => void;
}

interface ActionProps {
    type: string,
    payload?: any,
}

const initialState: GamesState = {
    fetching: false,
    updating: false,
};

const FETCH_GAMES_STARTED = 'FETCH_GAMES_STARTED';
const FETCH_GAMES_SUCCEEDED = 'FETCH_GAMES_SUCCEEDED';
const FETCH_GAMES_FAILED = 'FETCH_GAMES_FAILED';
const UPDATE_GAME_STARTED = 'UPDATE_GAME_STARTED';
const UPDATE_GAME_SUCCEDED = 'UPDATE_GAME_SUCCEDED';
const UPDATE_GAME_FAILED = 'UPDATE_GAMe_FAILED';
const SHOW_SUCCESS_MESSSAGE = 'SHOW_SUCCESS_MESSAGE';
const HIDE_SUCCESS_MESSSAGE = 'HIDE_SUCCESS_MESSAGE';
const CREATE_GAME_STARTED = 'CREATE_GAME_STARTED';
const CREATE_GAME_SUCCEDED = 'CREATE_GAME_SUCCEDED';
const CREATE_GAME_FAILED = 'CREATE_GAME_FAILED';
const DELETE_GAME_STARTED = 'DELETE_GAME_STARTED';
const DELETE_GAME_SUCCEDED = 'DELETE_GAME_SUCCEDED';
const DELETE_GAME_FAILED = 'DELETE_GAME_FAILED';

const reducer: (state: GamesState, action: ActionProps) => GamesState 
    = (state, { type, payload }) => {
    switch(type){
        case FETCH_GAMES_STARTED:
            return { ...state, fetching: true, fetchingError: null };
        case FETCH_GAMES_SUCCEEDED:
            return {...state, games: payload.games, fetching: false };
        case FETCH_GAMES_FAILED:
            return { ...state, fetchingError: payload.error, fetching: false };
        case UPDATE_GAME_STARTED:
            return { ...state, updateError: null, updating: true };
        case UPDATE_GAME_FAILED:
            return { ...state, updateError: payload.error, updating: false };
        case UPDATE_GAME_SUCCEDED:
            const games = [...(state.games || [])];
            const game = payload.game;
            const index = games.findIndex(it => it._id === game._id);
            games[index] = game;
            return { ...state,  games, updating: false };
        case CREATE_GAME_FAILED:
            console.log(payload.error);
          return { ...state, updateError: payload.error, updating: false };
        case CREATE_GAME_STARTED:
          return { ...state, updateError: null, updating: true };
        case CREATE_GAME_SUCCEDED:
            const beforeGames = [...(state.games || [])];
            const createdGame = payload.game;
            console.log(createdGame);
            const indexOfAdded = beforeGames.findIndex(it => it._id === createdGame._id || it.title === createdGame.title);
            console.log("index: ", indexOfAdded);
            if (indexOfAdded === -1) {
                beforeGames.splice(0, 0, createdGame);
            } else {
                beforeGames[indexOfAdded] = createdGame;
            }
            console.log(beforeGames);
            console.log(payload);
            return { ...state,  games: beforeGames, updating: false, updateError: null };
            case DELETE_GAME_FAILED:
              console.log(payload.error);
              return { ...state, updateError: payload.error, updating: false };
            case DELETE_GAME_STARTED:
              return { ...state, updateError: null, updating: true };
            case DELETE_GAME_SUCCEDED:
                const originalGames = [...(state.games || [])];
                const deletedGame = payload.game;
                const indexOfDeleted = originalGames.findIndex(it => it._id === deletedGame._id);
                if (indexOfDeleted > -1) {
                  originalGames.splice(indexOfDeleted, 1);
                }
                console.log(originalGames);
                console.log(payload);
                return { ...state,  games: originalGames, updating: false };
        case SHOW_SUCCESS_MESSSAGE:
            const allGames = [...(state.games || [])];
            const updatedGame = payload.updatedGame;
            const indexOfGame = allGames.findIndex(it => it._id === updatedGame._id);
            allGames[indexOfGame] = updatedGame;
            console.log(payload);
            return {...state, games: allGames, successMessage: payload.successMessage }
        case HIDE_SUCCESS_MESSSAGE:
            return {...state, successMessage: payload }
        
        default:
            return state;
    }
};

export const GameContext = React.createContext(initialState);

interface GameProviderProps {
    children: PropTypes.ReactNodeLike,
}

export const GameProvider: React.FC<GameProviderProps> = ({ children }) => {
    const [state, dispatch] = useReducer(reducer, initialState);
    const { games, fetching, fetchingError, updating, updateError, successMessage } = state;
    const { token } = useContext(AuthContext);
    const { networkStatus } = useNetwork();
    const [toast] = useIonToast();

    useEffect(getItemsEffect, [token]);
    useEffect(wsEffect, [token]);
    useEffect(executePendingOperations, [networkStatus.connected, token, toast]);

    const updateGame = useCallback<UpdateGameFn>(updateGameCallback, [token]);
    const addGame = useCallback<UpdateGameFn>(addGameCallback, [token]);
    const deleteGame = useCallback<DeleteGameFn>(deleteGameCallback, [token]);

    log('returns');

    function getItemsEffect() {
        let canceled = false;
        fetchItems();
        return () => {
            canceled = true;
        }

        async function fetchItems() {
          if(!token?.trim()){
            return;
          }

            try{
                log('fetchBooks started');
                dispatch({ type: FETCH_GAMES_STARTED });
                const games = await getAllGames(token);
                log('fetchItems succeeded');
                if (!canceled) {
                dispatch({ type: FETCH_GAMES_SUCCEEDED, payload: { games } });
                }
            } catch (error) {
                log('fetchItems failed');
                if (!canceled) {
                    dispatch({ type: FETCH_GAMES_FAILED, payload: { error } });
                }
            }
        }
    }

    async function updateGameCallback(game: GameProps) {
        try {
          log('updateGame started');
          dispatch({ type: UPDATE_GAME_STARTED });
          const updatedGame = await updateGameAPI(token, game);
          log('saveGame succeeded');
          dispatch({ type: UPDATE_GAME_SUCCEDED, payload: { game: updatedGame } });
        } catch (error: any) {
          log('updateGame failed');
          // save item to storage
          console.log('Updating game locally...');

          game.isNotSaved = true;
          await Preferences.set({
            key: `upd-${game.title}`,
            value: JSON.stringify({token, game })
          });
          dispatch({ type: UPDATE_GAME_SUCCEDED, payload: { game: game } });
          toast("You are offline... Updating game locally!", 3000);
    
          if(error.toJSON().message === 'Network Error')
            dispatch({ type: UPDATE_GAME_FAILED, payload: { error: new Error(error.response) } });
        }
    }

    async function addGameCallback(game: GameProps){
        try{
          log('addGame started');
          dispatch({ type: CREATE_GAME_STARTED });
          console.log(token);
          const addedGame = await createGameAPI(token, game);
          console.log(addedGame);
          log('saveGame succeeded');
          dispatch({ type: CREATE_GAME_SUCCEDED, payload: { game: addedGame } });
        }catch(error: any){
          log('addGame failed');
          console.log(error.response);
          // save item to storage
          console.log('Saving game locally...');
          const { keys } = await Preferences.keys();
          const matchingKeys = keys.filter(key => key.startsWith('sav-'));
          const numberOfItems = matchingKeys.length + 1;
          console.log(numberOfItems);

          game._id = numberOfItems.toString(); // ii adaug si id...
          game.isNotSaved = true;
          await Preferences.set({
            key: `sav-${game.title}`,
            value: JSON.stringify({token, game })
          });
          dispatch({ type: CREATE_GAME_SUCCEDED, payload: { game: game } });
          toast("You are offline... Saving game locally!", 3000);
    
          if(error.toJSON().message === 'Network Error')
            dispatch({ type: CREATE_GAME_FAILED, payload: { error: new Error(error.response || 'Network error') } });
        }
    }

    async function deleteGameCallback(id: string){
        try{
          log('deleteGame started');
          dispatch({ type: DELETE_GAME_STARTED });
          const deletedGame = await deleteGameAPI(token, id);
          console.log('deleted game: '+ deletedGame);
          log('deleteGame succeeded');
          dispatch({ type: DELETE_GAME_SUCCEDED, payload: { game: deletedGame } });
        }catch(error: any){
          log('addGame failed');
          console.log(error.response.data.message);
          dispatch({ type: DELETE_GAME_FAILED, payload: { error: new Error(error.response.data.message) } });
        }
    }

    function executePendingOperations(){
      async function helperMethod(){
          if(networkStatus.connected && token?.trim()){
              log('executing pending operations')
              const { keys } = await Preferences.keys();
              for(const key of keys) {
                  if(key.startsWith("sav-")){
                      const res = await Preferences.get({key: key});
                      console.log("Result", res);
                      if (typeof res.value === "string") {
                          const value = JSON.parse(res.value);
                          value.game._id=undefined;  // ca sa imi puna serverul id nou!!
                          log('creating item from pending', value);
                          await addGameCallback(value.game);
                          await Preferences.remove({key: key});
                      }
                  }
              }
              for(const key of keys) {
                if(key.startsWith("upd-")){
                    const res = await Preferences.get({key: key});
                    console.log("Result", res);
                    if (typeof res.value === "string") {
                        const value = JSON.parse(res.value);
                        log('updating item from pending', value);
                        await updateGameCallback(value.game);
                        await Preferences.remove({key: key});
                    }
                }
            }
          }
      }
      helperMethod();
  }

    function wsEffect() {
        let canceled = false;
        log('wsEffect - connecting');
        let closeWebSocket: () => void;
        if(token?.trim()){
          closeWebSocket = newWebSocket(token, message => {
            if (canceled) {
              return;
            }
            const { event, payload } = message;
            console.log('Provider message: ', message);

            log(`ws message, item ${event}`);
            if (event === 'updated') {
              console.log(payload);
              dispatch({ type: SHOW_SUCCESS_MESSSAGE, payload: {successMessage: payload.successMessage, updatedGame: payload.updatedGame } });
            }
            else if(event == 'created'){
              console.log(payload);
              dispatch({ type: CREATE_GAME_SUCCEDED, payload: { game: payload.updatedGame } });
            }
            else if(event === 'deleted'){
              console.log(payload);
              dispatch({ type: DELETE_GAME_SUCCEDED, payload: { game: payload.updatedGame } });
            }
          });
        }
        return () => {
          log('wsEffect - disconnecting');
          canceled = true;
          closeWebSocket?.();
        }
    }

    function closeShowSuccess(){
        dispatch({ type: HIDE_SUCCESS_MESSSAGE, payload: null });
    }

    const value = { games, fetching, fetchingError, updating, updateError, updateGame, addGame, deleteGame, successMessage, closeShowSuccess };

    return (
        <GameContext.Provider value={value}>
            {children}
        </GameContext.Provider>
    );
};

