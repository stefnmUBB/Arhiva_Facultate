import { useEffect, useState } from 'react';
import { App, AppState } from '@capacitor/app';
import { PluginListenerHandle } from '@capacitor/core';

const initialState = {
  isActive: true,
}

export const useAppState = () => {
  const [appState, setAppState] = useState(initialState)
  useEffect(function () {
    let handler: PluginListenerHandle;
    registerAppStateChange();
    App.getState().then(handleAppStateChange);
    let canceled = false;
    return () => {
      canceled = true;
      handler?.remove();
    }

    async function registerAppStateChange() {
      handler = await App.addListener('appStateChange', handleAppStateChange);
    }

    function handleAppStateChange(state: AppState) {
      console.log('useAppState - state change', state);
      if (!canceled) {
        setAppState(state);
      }
    }
  }, [])
  return { appState };
};
