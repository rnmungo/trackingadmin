import { arrayOf, number, shape, string } from 'prop-types';
import { MapContainer } from 'react-leaflet/MapContainer';
import { TileLayer } from 'react-leaflet/TileLayer';
import { Marker } from 'react-leaflet/Marker';
import { Popup } from 'react-leaflet/Popup';
import { Polyline } from 'react-leaflet/Polyline';
import { Icon } from 'leaflet';
import truckIcon from '../../assets/truck-2x.png';
import 'leaflet/dist/leaflet.css';

delete Icon.Default.prototype._getIconUrl;

Icon.Default.mergeOptions({
  iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
  iconUrl: require('leaflet/dist/images/marker-icon.png'),
  shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
});

const midIcon = new Icon({
  iconUrl: truckIcon,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41]
});

const WorldMap = ({ points = [], midPoint = null }) => (
  <MapContainer center={[points[0].lat, points[0].lng]} zoom={5} style={{ height: "100%", width: "100%" }}>
    <TileLayer
      url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    />
    {points.map(({ lat, lng, name }, idx) => (
      <Marker key={idx} position={[lat, lng]}>
        <Popup>{name}</Popup>
      </Marker>
    ))}
    {midPoint && (
      <Marker position={[midPoint?.lat, midPoint?.lng]} icon={midIcon}>
        <Popup>{midPoint?.name}</Popup>
      </Marker>
    )}
    <Polyline pathOptions={{ color: 'blue' }} positions={points.map(point => [point.lat, point.lng])} />
  </MapContainer>
);

WorldMap.propTypes = {
  points: arrayOf(shape({
    lat: number,
    lng: number,
    name: string,
  })),
  midPoint: shape({
    lat: number,
    lng: number,
    name: string,
  }),
};

export default WorldMap;
