import axios from "axios";
import { getLogger, authConfig, baseUrl, withLogs } from "../core";
import { GameProps } from "./GameProps";
import { Preferences } from "@capacitor/preferences";

const log = getLogger('gameLogger');

const gamesUrl = `http://${baseUrl}/api/game`;

export const getAllGames: (token: string) => Promise<GameProps[]> = (token) => {
    return withLogs(axios.get(gamesUrl, authConfig(token)), 'getAllGames');
}

export const updateGameAPI: (token: string, game: GameProps) => Promise<GameProps[]> = (token, game) => {
    return withLogs(axios.put(`${gamesUrl}/${game._id}`, game, authConfig(token)), 'updateGame');
}

export const createGameAPI: (token: string, game: GameProps) => Promise<GameProps[]> = (token, game) => {
  return withLogs(axios.post(`${gamesUrl}`, game, authConfig(token)), 'createGame');
}

export const deleteGameAPI: (token: string, id: string) => Promise<GameProps[]> = (token, id) => {
  return withLogs(axios.delete(`${gamesUrl}/${id}`, authConfig(token)), 'deleteGame');
}

interface MessageData {
    event: string;
    payload: {
      successMessage: string,
      updatedGame: GameProps[]
    };
}

export const newWebSocket = (token: string, onMessage: (data: MessageData) => void) => {
    const ws = new WebSocket(`ws://${baseUrl}`)
    ws.onopen = () => {
      log('web socket onopen');
      ws.send(JSON.stringify({type: 'authorization', payload :{token}}));
    };
    ws.onclose = () => {
      log('web socket onclose');
    };
    ws.onerror = error => {
      log('web socket onerror', error);
    };
    ws.onmessage = messageEvent => {
      log('web socket onmessage');
      console.log(messageEvent.data);
      onMessage(JSON.parse(messageEvent.data));
    };
    return () => {
      ws.close();
    }
}

  