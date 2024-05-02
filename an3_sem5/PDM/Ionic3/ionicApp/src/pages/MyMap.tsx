import React, { useState } from "react";
import { GoogleMap, Marker } from '@capacitor/google-maps';
import { compose, withProps } from 'react-recompose';
import { mapsApiKey } from './mapsApiKey';
import { useRef, useEffect } from 'react';
import { IonButton } from '@ionic/react';

interface MyMapProps {
    lat?: number;
    lng?: number;
    onCoordsChanged: (lat:number, lng:number)=>void,    
}

import { useAppState } from "./useAppState";
import { useNetwork } from "./useNetwork";
import { IonItem, IonLabel } from "@ionic/react";


export const MyMap: React.FC<MyMapProps> = ({lat, lng, onCoordsChanged}) => {
    const mapRef = useRef<HTMLElement>();    
    let newMap: GoogleMap;
    let markerId : string;
  
    const [latitude, setLatitude ] = useState<number | undefined>(lat);    
    const [longitude, setLongitude] = useState<number | undefined>(lng);
    const [mapLoaded, setMapLoaded] = useState<boolean>(false);

    console.log(`init lat = ${latitude}`);

    useEffect(()=>{
        console.log("LL changed!!!!!!!!!!!!!!!!!!!!");
        setLatitude(lat);
        setLongitude(lng);
        console.log(`LLL ${latitude}`);
        ()=>{ async()=>{
            if(latitude && longitude){
                markerId = await newMap.addMarker({
                    coordinate: {
                    lat: latitude,
                    lng: longitude
                    }                                
                });
            }
        }}
    }, [lat, lng, mapLoaded]);

    async function createMap() {
        if (!mapRef.current) return;
        newMap = await GoogleMap.create({
            id: 'my-cool-map',
            element: mapRef.current,
            apiKey: mapsApiKey,
            config: {
                center: {
                lat: latitude || 0,
                lng: longitude || 0
                },
                zoom: 8
            }
        })        
        setMapLoaded(true);
        

        if(latitude && longitude){
            markerId = await newMap.addMarker({
                coordinate: {
                lat: latitude,
                lng: longitude
                }                                
            });
        }

        await newMap.setOnMapClickListener(async (event)=>{                        
            console.log(`LL ${event.latitude} ${event.longitude}`); 
            newMap.removeMarker(markerId);
            markerId = await newMap.addMarker({
                coordinate: {
                lat: event.latitude,
                lng: event.longitude
                }                                
            });
            onCoordsChanged(event.latitude, event.longitude);               
        });                          
    }    
    
  
    return (
      <div className="component-wrapper">
        <IonButton style={{ padding:3 }} onClick={createMap}>Create Map</IonButton>        
        <capacitor-google-map ref={mapRef} style={{
          display: 'inline-block',
          width: "100%",
          height: 400
        }}>


        </capacitor-google-map>        
      </div>
    )
  }
  
  //export default MyMap;