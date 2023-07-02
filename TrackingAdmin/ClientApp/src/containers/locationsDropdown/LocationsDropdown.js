import { func, string } from 'prop-types';
import MuiStack from '@mui/material/Stack';
import { Dropdown, MultiSelectDropdown } from '../../components/dropdown';
import { useQueryLocations } from '../../hooks/useQueries';

const LocationsDropdownContainer = ({
  selectedOrigin = '',
  selectedDestinations = [],
  onChangeOrigin = () => { },
  onChangeDestinations = () => { }
}) => {
  const originLocations = useQueryLocations({ key: 'origin' });
  const destinationLocations = useQueryLocations({ key: 'destination' });

  return (
    <MuiStack direction="row" spacing={2}>
      <Dropdown
        sx={{ width: 200 }}
        label="Origen"
        ariaLabel="origin-location"
        helperText={originLocations.status === 'error' && (originLocations.error?.response?.data?.message || originLocations.error?.message)}
        value={selectedOrigin}
        items={originLocations.data ? originLocations.data.map(({ id, name }) => ({ id, text: name })) : []}
        onChange={onChangeOrigin}
      />
      <MultiSelectDropdown
        sx={{ width: 470 }}
        label="Destinos"
        ariaLabel="destinations-location"
        helperText={destinationLocations.status === 'error' && (destinationLocations.error?.response?.data?.message || destinationLocations.error?.message)}
        value={selectedDestinations}
        items={destinationLocations.data ? destinationLocations.data.map(({ id, name }) => ({ id, text: name })) : []}
        excludeItems={[selectedOrigin]}
        onChange={onChangeDestinations}
      />
    </MuiStack>
  );
};

LocationsDropdownContainer.propTypes = {
  value: string,
  onChange: func,
};

export default LocationsDropdownContainer;
