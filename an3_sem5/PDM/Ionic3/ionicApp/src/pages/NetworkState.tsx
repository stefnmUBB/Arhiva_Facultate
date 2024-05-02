import React from 'react';
import { useAppState } from "./useAppState";
import { useNetwork } from "./useNetwork";
import { IonItem, IonLabel } from "@ionic/react";


export const NetworkState: React.FC = () => {
    const { appState } = useAppState();
    const {networkStatus} = useNetwork();
    
    return (      
        <IonLabel style={{ marginRight: '5px' }} slot="end">
            Connected: <b style={{color:(networkStatus.connected?"green":"red")}}>•</b>        
        </IonLabel>
    );
}