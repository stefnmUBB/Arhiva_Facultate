import React, { memo } from "react";
import { IonItem, IonLabel, IonNote } from "@ionic/react";
import { getLogger } from "../core";
import { GameProps } from "./GameProps";
import styles from "./styles.module.css";

interface GamePropsExtended extends GameProps {
    onEdit: (_id?: string) => void;
}

const GameComponent: React.FC<GamePropsExtended> = ({_id, title, launchDate, platform, lastVersion, url, authors, totalReleases, isOpenSource, isNotSaved, onEdit }) => (  
    <IonItem style={{color:isNotSaved?"red":undefined, height:"20vw"}} onClick={() => onEdit(_id)}>
      <IonNote style={{"width":"5vw"}}>{platform}</IonNote>
      <IonLabel><b>{title}</b> V <i>{lastVersion}</i> by <i>{authors.join(", ")}</i></IonLabel>      
      <br/>
      <IonLabel>launched on {launchDate ? new Date(launchDate).toISOString().split("T")[0] : "(Invalid date)"} {isOpenSource ? "(Open source)" : ""}</IonLabel>      
    </IonItem>
);

export default memo(GameComponent);
