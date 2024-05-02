import React, { memo, version } from 'react';
import { IonItem, IonLabel, IonNote } from '@ionic/react';
import { getLogger } from '../core';
import { ItemProps } from './ItemProps';

const log = getLogger('Item');

interface ItemPropsExt extends ItemProps {
  onEdit: (id?: string) => void;
}

const Item: React.FC<ItemPropsExt> = ({ id, title, platform, url, authors, lastVersion, launchDate, isOpenSource, onEdit }) => {
  return (
    <IonItem onClick={() => onEdit(id)}>
      <IonNote style={{"width":"5vw"}}>{platform}</IonNote>
      <IonLabel><b>{title}</b> V <i>{lastVersion}</i> by <i>{authors.join(", ")}</i></IonLabel>      
      <br/>
      <IonLabel>launched on {new Date(launchDate).toISOString().split("T")[0]} {isOpenSource ? "(Open source)" : ""}</IonLabel>      
    </IonItem>
  );
};

export default memo(Item);
