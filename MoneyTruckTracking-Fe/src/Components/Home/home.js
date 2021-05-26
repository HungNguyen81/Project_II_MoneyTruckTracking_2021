import React, { useState, useEffect } from 'react';
import * as truckData from "../../data/trucks.json";
import './home-style.css';
// import {useHistory} from 'react-router-dom';
import {
    withGoogleMap,
    withScriptjs,
    GoogleMap,
    Marker,
    InfoWindow,
    Polyline
} from 'react-google-maps';


function Map() {
    const [selectedTruck, setselectedTruck] = useState(null);
    var pathCoordinates = [
        { lat: 21.005119917797476, lng: 105.84385323229442 },
        { lat: 21.005149965457047, lng: 105.84460425080465 },
        { lat: 21.006086447814276, lng: 105.84462570847639 }        
    ]
    useEffect(() => {
        const listener = e => {
            if (e.key === "Escape") {
                setselectedTruck(null);
            }
        };
        window.addEventListener("keydown", listener);

        return () => {
            window.removeEventListener("keydown", listener);
        };
    }, []);

    return (
        <GoogleMap
            defaultZoom={17}
            defaultCenter={{
                lat: 21.00578570922146,
                lng: 105.8444336079662
            }}
        >
            {truckData.features.map(truck => (
                <Marker
                    key={truck.truck_ID}
                    position={{
                        lat: truck.lat,
                        lng: truck.lng
                    }}
                    onClick={() => {
                        setselectedTruck(truck);
                    }}
                    icon={{
                        url: `/truck.png`,
                        scaledSize: new window.google.maps.Size(50, 50)
                    }}
                />
            ))}
            
            <Polyline
                path={pathCoordinates}
                geodesic={true}
                options={{
                    strokeColor: "#ff0000",
                    strokeOpacity: 0.3,
                    strokeWeight: 5,                    
                }}
            />

            {selectedTruck && (
                <InfoWindow                
                    onCloseClick={() => {
                        setselectedTruck(null);
                    }}
                    position={{
                        lat: selectedTruck.lat,
                        lng: selectedTruck.lng
                    }}
                >
                    <div>
                        <h2>{selectedTruck.content}</h2>
                        <strong>{selectedTruck.truck_id}</strong>
                        <br/>
                        <p>{selectedTruck.lat}, {selectedTruck.lng}</p>
                    </div>
                </InfoWindow>
            )}
        </GoogleMap>
    );
}

const MapWrapped = withScriptjs(withGoogleMap(Map));

export default function Home() {
    return (
        <div style={{ width: "100vw", height: "100vh" }}>
            <MapWrapped
                googleMapURL={`https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places`}
                loadingElement={<div style={{ height: `100%` }} />}
                containerElement={<div style={{ height: `100%` }} />}
                mapElement={<div style={{ height: `100%` }} />}
            />
        </div>
    );
}

// const Home = () => {    
//     // const history = useHistory();
//     return(
//         // <>        
//         // <h1 className="homeTitle">HI <i>{history.location.state.data.name}</i>! THIS IS HOME PAGE </h1>        
//         // </>
//         <div>
//             Hello
//         </div>
//     )
// }

// export default Home;