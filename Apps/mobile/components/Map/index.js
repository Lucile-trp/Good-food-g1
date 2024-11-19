import React, { useState, useEffect } from "react";
import MapView, { Marker } from "react-native-maps";
import { StyleSheet, View } from "react-native";
import * as Location from 'expo-location';
import {API_URL} from '@env'

const MapWithPlaces = () => {
  const [places, setPlaces] = useState([]);
  const [center, setCenter] = useState({ latitude: 0, longitude: 0 });
  const [zoom, setZoom] = useState(12);
  const [visiblePlaces, setVisiblePlaces] = useState([]);
  const [data, setData] = useState([]);
  const [region, setRegion] = useState({
    latitude: 48.8566,
    longitude: 2.3522,
    latitudeDelta: 0.0922,
    longitudeDelta: 0.0421,
  });
  const [errorMsg, setErrorMsg] = useState(null);
  
  useEffect(() => {
    const apiUrl = `${API_URL}/lieux`;
    
    fetch(apiUrl)
      .then((response) => response.json())
      .then((data) => {
        const samplePlaces = data.map((place) => ({
          nom: place.nom,
          description: place.description,
          latitude: place.latitude,
          longitude: place.longitude,
          img: place.image,
        }));
        setPlaces(samplePlaces);
      })
      .catch((error) => console.log("error", error));
  }, []);

  useEffect(() => {
    (async () => {
      let { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== 'granted') {
        setErrorMsg('Permission to access location was denied');
        return;
      }
      
      let location = await Location.getCurrentPositionAsync({});
      setRegion({
        ...region,
        latitude: location.coords.latitude,
        longitude: location.coords.longitude,
      });
    })();
  }, []);
  
  const filterVisiblePlaces = (region) => {
    const visiblePlaces = places.filter(
      (place) =>
        place.latitude <= region.latitude + region.latitudeDelta / 2 &&
        place.latitude >= region.latitude - region.latitudeDelta / 2 &&
        place.longitude <= region.longitude + region.longitudeDelta / 2 &&
        place.longitude >= region.longitude - region.longitudeDelta / 2
    );
    setVisiblePlaces(visiblePlaces);
  };

  const handleRegionChangeComplete = (region) => {
    filterVisiblePlaces(region);
  };

  return (
    <View style={styles.container}>
      {/* <MapView
        style={styles.map}
        region={region}
        onRegionChangeComplete={region => setRegion(region)}
      >
        <Marker
        coordinate={{ latitude: region.latitude, longitude: region.longitude }}
        />
        {visiblePlaces.map((place, index) => (
          <Marker
            key={index}
            coordinate={{ latitude: place.latitude, longitude: place.longitude }}
            title={place.nom}
            description={place.description}
          />
        ))}
      </MapView> */}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    ...StyleSheet.absoluteFillObject,
    justifyContent: "flex-end",
    alignItems: "center",
  },
  map: {
    ...StyleSheet.absoluteFillObject,
  },
});

export default MapWithPlaces;