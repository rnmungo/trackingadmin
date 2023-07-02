import { Link } from 'react-router-dom';
import MuiButton from '@mui/material/Button';
import { isWebUri } from 'valid-url';

const ExternalLink = ({ to, children, ...buttonProps }) => {
  const handleClick = (event) => {
    event.preventDefault();

    if (isWebUri(to) && to.startsWith('https://')) {
      window.open(to, '_blank', 'noopener,noreferrer');
    }
  };

  return (
    <MuiButton
      component={Link}
      to="#"
      onClick={handleClick}
      {...buttonProps}
    >
      {children}
    </MuiButton>
  );
};

export default ExternalLink;
