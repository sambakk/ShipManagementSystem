import {
	Home as HomeIcon,
	BarChartOutlined as DashboardIcon,
} from '@mui/icons-material';

import { Home } from '../pages/Home';

import { Route } from '../types/Route';

const routes: Array<Route> = [
	{
		key: 'router-home',
		title: 'Home',
		description: 'Home',
		component: Home,
		path: '/',
		isEnabled: true,
		icon: HomeIcon,
		appendDivider: true,
	}
];

export default routes;
