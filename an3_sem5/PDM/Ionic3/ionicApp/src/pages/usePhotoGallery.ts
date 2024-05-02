import { Camera, Photo as CameraPhoto, CameraResultType, CameraSource } from '@capacitor/camera';
import { Filesystem, Directory } from '@capacitor/filesystem';
//import { Filesystem, Directory } from '@capacitor/storage';
import { Preferences } from '@capacitor/preferences';
import { Capacitor } from '@capacitor/core';
import { useEffect, useState } from 'react';

export interface Photo {
    filepath: string;
    webviewPath?: string;
}

const PHOTO_STORAGE = 'photos';

const { deleteFile, readFile, writeFile } = Filesystem;
const { get, set } = Preferences;


export function usePhotoGallery() {
    const [photos, setPhotos] = useState<Photo[]>([]);    

    const takePhoto = async () => {
        try
        {    
            const cameraPhoto = await Camera.getPhoto({
                resultType: CameraResultType.Uri,
                source: CameraSource.Camera,
                quality: 100
            });
            const fileName = new Date().getTime() + '.jpeg';
            const savedFileImage = await savePicture(cameraPhoto, fileName);
            const newPhotos = [savedFileImage, ...photos];
            setPhotos(newPhotos);
            Preferences.set({key: PHOTO_STORAGE, value: JSON.stringify(newPhotos)});            
            return savedFileImage.webviewPath;            
        } catch{
            return "";
        }
    };    

    const savePicture = async (photo: CameraPhoto, fileName: string): Promise<Photo> => {
        const base64Data = await base64FromPath(photo.webPath!);
        await writeFile({
            path: fileName,
            data: base64Data,
            directory: Directory.Data,
            recursive: true
        });

        return {
            filepath: fileName,
            webviewPath: base64Data
        };
    };    

    useEffect(() => {
        const loadSaved = async () => {
            const photosString = (await get({key:PHOTO_STORAGE})).value;
            const photos = (photosString ? JSON.parse(photosString) : []) as Photo[];
            for (let photo of photos) {
                try {
                    const file = await readFile({
                        path: photo.filepath,
                        directory: Directory.Data
                    });
                    photo.webviewPath = `data:image/jpeg;base64,${file.data}`;
                } catch (error) {
                    setPhotos([]);
                    return;
                }
            }
            setPhotos(photos);
        };
        loadSaved();
    }, [get, readFile]);


    async function readAsBase64(cameraPhoto: CameraPhoto) {
        // Fetch the photo, read as a blob, then convert to base64 format
        const response = await fetch(cameraPhoto.webPath!);
        const blob = await response.blob();

        return await convertBlobToBase64(blob) as string;
    }

    const  convertBlobToBase64 = (blob: Blob) => new Promise((resolve, reject) => {
        const reader = new FileReader;
        reader.onerror = reject;
        reader.onload = () => {
            resolve(reader.result);
        };
        reader.readAsDataURL(blob);
    });

    async function base64FromPath(path: string): Promise<string> {
        const response = await fetch(path);
        const blob = await response.blob();
        //const blob = new Blob(neu)
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            console.log(reader)
            reader.onerror = reject;
            reader.onload = () => {
            if (typeof reader.result === 'string') {
                resolve(reader.result);
            } else {
                reject('method did not return a string')
            }
            };
            reader.readAsDataURL(blob);
        });
    }

    const deletePhoto = async (photo: Photo) => {
        const newPhotos = photos.filter(p => p.filepath !== photo.filepath);
        set({key:PHOTO_STORAGE, value:JSON.stringify(newPhotos)});
        const filename = photo.filepath.substring(photo.filepath.lastIndexOf('/') + 1);
        await deleteFile({
            path: filename,
            directory: Directory.Data
        });
        setPhotos(newPhotos);
    };    


    return {
        photos: photos,
        takePhoto: takePhoto,
        deletePhoto: deletePhoto
    };
  }  
