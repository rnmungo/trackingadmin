import { useCallback, useEffect, useState } from 'react';
import MuiLoadingButton from '@mui/lab/LoadingButton';
import MuiBox from '@mui/material/Box';
import MuiStack from '@mui/material/Stack';
import {
  useMutationCalibrateDistances,
  useMutationCreateRoadMap
} from '../../hooks/useMutations';
import { useSnackbar } from '../../contexts/snackbar';
import { WorldMap } from '../../components/worldMap';
import { AllowedTrucksDropdownContainer } from '../allowedTrucksDropdown';
import { LocationsDropdownContainer } from '../locationsDropdown';

const RoadMapFormContainer = () => {
  const [truckState, setTruckState] = useState('');
  const [distanceState, setDistanceState] = useState(null);
  const [originState, setOriginState] = useState('');
  const [destinationState, setDestinationState] = useState([]);

  const calibrate = useMutationCalibrateDistances();
  const createRoadMap = useMutationCreateRoadMap();
  const snackbar = useSnackbar();

  const cleanForm = useCallback(() => {
    setTruckState('');
    setDistanceState(null);
    setOriginState('');
    setDestinationState([]);
  }, []);

  const handleOriginChange = useCallback(value => {
    setOriginState(value);
  }, []);

  const handleDestinationChange = useCallback(value => {
    setDestinationState(value);
  }, []);

  const handleTruckChange = useCallback(value => {
    setTruckState(value);
  }, []);

  const handleClickCalibrate = useCallback(() => {
    if (originState && destinationState.length > 0) {
      calibrate.mutate({
        originId: originState,
        destinationIds: destinationState
      });
    }
  }, [calibrate, originState, destinationState]);

  const handleClickCreate = useCallback(() => {
    if (truckState && distanceState) {
      createRoadMap.mutate({
        truckId: truckState,
        travels: distanceState.map(distance => ({ distanceId: distance.id }))
      });
    }
  }, [createRoadMap, truckState, distanceState]);

  useEffect(() => {
    setDestinationState([]);
  }, [originState]);

  useEffect(() => {
    if (calibrate.status === 'success') {
      setDistanceState(calibrate.data);
      calibrate.reset();
    }

    if (calibrate.status === 'error') {
      snackbar.error(
        calibrate.error.response?.data?.message || calibrate.error.message
      );
      calibrate.reset();
    }
  }, [calibrate, snackbar]);

  useEffect(() => {
    if (createRoadMap.status === 'success') {
      snackbar.success("¡Operación realizada con éxito!");
      createRoadMap.reset();
      cleanForm();
    }

    if (createRoadMap.status === 'error') {
      snackbar.error(
        createRoadMap.error.response?.data?.message || createRoadMap.error.message
      );
      createRoadMap.reset();
    }
  }, [cleanForm, createRoadMap, snackbar]);

  const getPoints = distances => {
    let points = [];
    points = distances.map(distance => ({
      name: distance.originLocation.name,
      lat: distance.originLocation.latitude,
      lng: distance.originLocation.longitude,
    }));
    const lastDistance = distances[distances.length - 1];
    points.push({
      name: lastDistance.destinationLocation.name,
      lat: lastDistance.destinationLocation.latitude,
      lng: lastDistance.destinationLocation.longitude,
    });
    return points;
  };

  return (
    <MuiStack spacing={4}>
      <MuiStack direction="row" spacing={2}>
        <LocationsDropdownContainer
          selectedOrigin={originState}
          selectedDestinations={destinationState}
          onChangeOrigin={handleOriginChange}
          onChangeDestinations={handleDestinationChange}
        />
        <MuiLoadingButton
          aria-label="Button to calibrate"
          loading={calibrate.status === 'loading'}
          loadingIndicator="Buscando"
          color="primary"
          variant="contained"
          onClick={handleClickCalibrate}
        >
          Buscar ruta
        </MuiLoadingButton>
      </MuiStack>
      <MuiStack direction="row" spacing={2}>
        <AllowedTrucksDropdownContainer
          onChange={handleTruckChange}
          value={truckState}
        />
        <MuiLoadingButton
          aria-label="Button to create"
          loading={createRoadMap.status === 'loading'}
          loadingIndicator="Procesando"
          color="primary"
          variant="contained"
          onClick={handleClickCreate}
        >
          Cargar ruta
        </MuiLoadingButton>
      </MuiStack>
      {distanceState && (
        <MuiBox sx={{ width: "100%", height: 680 }}>
          <WorldMap
            points={getPoints(distanceState)}
          />
        </MuiBox>
      )}
    </MuiStack>

  );
};

export default RoadMapFormContainer;
