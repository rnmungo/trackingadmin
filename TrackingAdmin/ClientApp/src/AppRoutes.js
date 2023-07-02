import { RoadMapReportPage, NewRoadMapPage } from "./pages/roadMap";

const AppRoutes = [
  {
    index: true,
    path: '/',
    element: <RoadMapReportPage />
  },
  {
    index: true,
    path: '/new-road-map',
    element: <NewRoadMapPage />
  },
];

export default AppRoutes;
