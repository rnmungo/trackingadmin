import { forwardRef, useCallback, useState } from 'react';
import { arrayOf, number, shape, string } from 'prop-types';
import get from 'lodash.get';
import MuiBox from '@mui/material/Box';
import MuiAppBar from '@mui/material/AppBar';
import MuiToolbar from '@mui/material/Toolbar';
import MuiButton from '@mui/material/Button';
import MuiCollapse from '@mui/material/Collapse';
import MuiIconButton from '@mui/material/IconButton';
import MuiDialog from '@mui/material/Dialog';
import MuiPaper from '@mui/material/Paper';
import MuiSlide from '@mui/material/Slide';
import MuiStack from '@mui/material/Stack';
import MuiTable from '@mui/material/Table';
import MuiTableBody from '@mui/material/TableBody';
import MuiTableCell from '@mui/material/TableCell';
import MuiTableHead from '@mui/material/TableHead';
import MuiTableRow from '@mui/material/TableRow';
import MuiTypography from '@mui/material/Typography';
import MuiKeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import MuiKeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import MuiCloseIcon from '@mui/icons-material/Close';
import { WorldMap } from '../../../components/worldMap';
import useDateFormatter from '../../../hooks/useDateFormatter';
import { STATUS } from '../../../constants';

const MuiTransition = forwardRef(function Transition(props, ref) {
  return <MuiSlide direction="up" ref={ref} {...props} />;
});

const ReportTableRow = ({ row }) => {
  const [openState, setOpenState] = useState(false);
  const [collapsedState, setCollapsedState] = useState(false);
  const { getDateTimeLocalString } = useDateFormatter();

  const handleClose = useCallback(() => {
    setOpenState(false);
  }, []);

  const handleOpen = useCallback(() => {
    setOpenState(true);
  }, []);

  const getPoints = travels => {
    let points = [];
    points = travels.map(travel => ({
      name: travel.distance.originLocation.name,
      lat: travel.distance.originLocation.latitude,
      lng: travel.distance.originLocation.longitude,
    }));
    const lastDistance = travels[travels.length - 1].distance;
    points.push({
      name: lastDistance.destinationLocation.name,
      lat: lastDistance.destinationLocation.latitude,
      lng: lastDistance.destinationLocation.longitude,
    });
    return points;
  };

  const getMidPoint = travels => {
    const inProcessTravel = travels.find(travel => travel.status === 'InProgress');
    if (inProcessTravel) {
      console.log(inProcessTravel);
      const start = inProcessTravel.distance.originLocation;
      const end = inProcessTravel.distance.destinationLocation;
      const progress = 0.5;

      return {
        lat: start.latitude + (end.latitude - start.latitude) * progress,
        lng: start.longitude + (end.longitude - start.longitude) * progress,
        name: `${row.truck.model} (${row.truck.licensePlate})`,
      };
    }

    return null;
  };

  return (
    <>
      <MuiTableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <MuiTableCell>
          <MuiIconButton
            aria-label="expand row"
            size="small"
            onClick={() => setCollapsedState(prevState => !prevState)}
          >
            {collapsedState ? <MuiKeyboardArrowUpIcon /> : <MuiKeyboardArrowDownIcon />}
          </MuiIconButton>
        </MuiTableCell>
        <MuiTableCell component="th" scope="row" align="left">
          <MuiStack direction="column">
            <MuiTypography component="span" variant="body1">
              {`# ${get(row, 'number', '-')}`}
            </MuiTypography>
            <MuiTypography component="span" variant="body2" color="grey">
              {getDateTimeLocalString(new Date(row.createdAt))}
            </MuiTypography>
          </MuiStack>
        </MuiTableCell>
        <MuiTableCell align="left">
          <MuiStack direction="column">
            <MuiTypography component="span" variant="body1">
              {get(row, 'truck.model', '-')}
            </MuiTypography>
            <MuiTypography component="span" variant="body2" color="grey">
              {get(row, 'truck.licensePlate', '-')}
            </MuiTypography>
          </MuiStack>
        </MuiTableCell>
        <MuiTableCell align="left">
          <MuiTypography component="span" variant="body1">
            {STATUS.find(status => status.id === row?.status)?.text ?? '-'}
          </MuiTypography>
        </MuiTableCell>
        <MuiTableCell align="left">
          <MuiButton
            variant="contained"
            size="small"
            color="primary"
            onClick={handleOpen}
          >
            Ver en mapa
          </MuiButton>
        </MuiTableCell>
      </MuiTableRow>
      <MuiTableRow>
        <MuiTableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <MuiCollapse in={collapsedState} timeout="auto" unmountOnExit>
            <MuiBox sx={{ my: 2, p: 2 }} component={MuiPaper}>
              <MuiTypography variant="h6" gutterBottom component="div">
                Detalle del recorrido
              </MuiTypography>
              <MuiTable size="small" aria-label="report">
                <MuiTableHead>
                  <MuiTableRow>
                    <MuiTableCell align="right">#</MuiTableCell>
                    <MuiTableCell align="left">Desde</MuiTableCell>
                    <MuiTableCell align="left">Hasta</MuiTableCell>
                    <MuiTableCell align="left">Estado</MuiTableCell>
                    <MuiTableCell align="left">Fecha de inicio</MuiTableCell>
                    <MuiTableCell align="right">Kms</MuiTableCell>
                  </MuiTableRow>
                </MuiTableHead>
                <MuiTableBody>
                  {row.travels.map(travel => (
                    <MuiTableRow key={travel.id}>
                      <MuiTableCell align="right">
                        {get(travel, 'orderNumber', '-')}
                      </MuiTableCell>
                      <MuiTableCell component="th" scope="row" align="left">
                        {get(travel, 'distance.originLocation.name', '-')}
                      </MuiTableCell>
                      <MuiTableCell align="left">
                        {get(travel, 'distance.destinationLocation.name', '-')}
                      </MuiTableCell>
                      <MuiTableCell align="left">
                        {STATUS.find(status => status.id === travel?.status)?.text ?? '-'}
                      </MuiTableCell>
                      <MuiTableCell align="left">
                        {travel.startDate ? getDateTimeLocalString(new Date(travel.startDate)) : ''}
                      </MuiTableCell>
                      <MuiTableCell align="right">
                        {get(travel, 'distance.distanceInKm', '-')}
                      </MuiTableCell>
                    </MuiTableRow>
                  ))}
                </MuiTableBody>
              </MuiTable>
            </MuiBox>
          </MuiCollapse>
        </MuiTableCell>
      </MuiTableRow>
      <MuiDialog
        fullScreen
        open={openState}
        onClose={handleClose}
        TransitionComponent={MuiTransition}
      >
        <MuiAppBar sx={{ position: 'relative' }}>
          <MuiToolbar>
            <MuiIconButton
              edge="start"
              color="inherit"
              onClick={handleClose}
              aria-label="close"
            >
              <MuiCloseIcon />
            </MuiIconButton>
            <MuiTypography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              Mapa
            </MuiTypography>
          </MuiToolbar>
        </MuiAppBar>
        {openState && (
          <MuiBox sx={{ width: "100%", height: 680 }}>
            <WorldMap
              points={getPoints(row.travels)}
              midPoint={getMidPoint(row.travels)}
            />
          </MuiBox>
        )}
      </MuiDialog>
    </>
  );
};

ReportTableRow.propTypes = {
  row: shape({
    number: number,
    createdAt: string,
    status: string,
    truck: shape({
      licensePlate: string,
      model: string,
    }),
    travels: arrayOf(shape({
      id: string,
      startDate: string,
      status: string,
      distance: shape({
        id: string,
        distanceInKm: number,
        originLocation: shape({
          id: string,
          name: string,
        }),
        destinationLocation: shape({
          id: string,
          name: string,
        }),
      }),
    }))
  }),
};

export default ReportTableRow;
