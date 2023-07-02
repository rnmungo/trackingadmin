import { useCallback, useEffect, useState } from 'react';
import { arrayOf, func, shape, string } from 'prop-types';
import MuiBox from '@mui/material/Box';
import MuiInputLabel from '@mui/material/InputLabel';
import MuiMenuItem from '@mui/material/MenuItem';
import MuiFormControl from '@mui/material/FormControl';
import MuiFormHelperText from '@mui/material/FormHelperText';
import MuiSelect from '@mui/material/Select';

const Dropdown = ({ label, ariaLabel = '', value = '', helperText = '', items = [], excludeItems = [], onChange = () => {}, ...containerProps }) => {
  const [valueState, setValueState] = useState(value);

  const handleChange = useCallback(event => {
    onChange(event.target.value);
    setValueState(event.target.value);
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
          value={valueState}
          label={label}
          onChange={handleChange}
        >
          {items && items.filter(item => !excludeItems.includes(item.id)).map(item => (
            <MuiMenuItem
              key={item.id}
              value={item.id}
              selected={item.id === valueState}
            >
              {item.text}
            </MuiMenuItem>
          ))}
        </MuiSelect>
        {helperText && <MuiFormHelperText>{helperText}</MuiFormHelperText>}
      </MuiFormControl>
    </MuiBox>
  );
};

Dropdown.propTypes = {
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

export default Dropdown;
