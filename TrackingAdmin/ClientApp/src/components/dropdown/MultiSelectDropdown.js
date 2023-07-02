import { useCallback, useEffect, useState } from 'react';
import { arrayOf, func, shape, string } from 'prop-types';
import MuiBox from '@mui/material/Box';
import MuiOutlinedInput from '@mui/material/OutlinedInput';
import MuiInputLabel from '@mui/material/InputLabel';
import MuiMenuItem from '@mui/material/MenuItem';
import MuiListItemText from '@mui/material/ListItemText';
import MuiFormControl from '@mui/material/FormControl';
import MuiFormHelperText from '@mui/material/FormHelperText';
import MuiSelect from '@mui/material/Select';
import MuiChip from '@mui/material/Chip';
import MuiCheckbox from '@mui/material/Checkbox';


const MultiSelectDropdown = ({ label, ariaLabel = '', value = [], helperText = '', items = [], excludeItems = [], onChange = () => { }, ...containerProps }) => {
  const [valueState, setValueState] = useState(value ?? []);

  const handleChange = useCallback(event => {
    const { value: newValue } = event.target;
    const updatedValue = typeof newValue === 'string' ? newValue.split(',') : newValue
    onChange(updatedValue);
    setValueState(updatedValue);
  }, [onChange]);

  useEffect(() => {
    setValueState(value);
  }, [value]);

  return (
    <MuiBox {...containerProps}>
      <MuiFormControl fullWidth>
        <MuiInputLabel id={ariaLabel}>{label}</MuiInputLabel>
        <MuiSelect
          labelId={ariaLabel}
          id={ariaLabel}
          multiple
          value={valueState}
          label={label}
          input={<MuiOutlinedInput label="Tag" />}
          onChange={handleChange}
          renderValue={selectedIds => (
            <MuiBox sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
              {selectedIds.map(selectedId => (
                <MuiChip key={selectedId} label={items.find(item => item.id === selectedId)?.text} />
              ))}
            </MuiBox>
          )}
        >
          {items && items.filter(item => !excludeItems.includes(item.id)).map(item => (
            <MuiMenuItem
              key={item.id}
              value={item.id}
              selected={item.id === valueState}
            >
              <MuiCheckbox checked={valueState.indexOf(item.id) > -1} />
              <MuiListItemText primary={item.text} />
            </MuiMenuItem>
          ))}
        </MuiSelect>
        {helperText && <MuiFormHelperText>{helperText}</MuiFormHelperText>}
      </MuiFormControl>
    </MuiBox>
  );
};

MultiSelectDropdown.propTypes = {
  label: string.isRequired,
  ariaLabel: string,
  value: string,
  items: arrayOf(shape({
    id: string,
    text: string,
  })),
  excludeItems: arrayOf(string),
  onChange: func,
};

export default MultiSelectDropdown;
