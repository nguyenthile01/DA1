import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
    badge: {
      variant: 'info',
      text: 'NEW'
    }
  },
  {
    name: 'User',
    url: '/user',
    icon: 'icon-cursor',
    children: [
      {
        name: 'Admin',
        url: '/user/admin',
        icon: 'icon-cursor'
      },
      {
        name: 'Employer',
        url: '/user/employer',
        icon: 'icon-cursor'
      },
      {
        name: 'JobSeeker',
        url: '/user/jobSeeker',
        icon: 'icon-cursor'
      }
    ]
  },
  {
    title: true,
    name: 'Danh mục'
  },
  {
    name: 'Quản lý user',
    url: '/admin',
    icon: 'icon-puzzle',
    children: [
      {
        name: 'Admin',
        url: '/admin/admin',
        icon: 'icon-puzzle'
      },
      // {
      //   name: 'Employers',
      //   url: '/admin/admin',
      //   icon: 'icon-puzzle'
      // },
      // {
      //   name: 'JobSeeker',
      //   url: '/admin',
      //   icon: 'icon-puzzle'
      // },
    ]
  },
  {
    name: 'Quản lý emloyer',
    url: '/buttons',
    icon: 'icon-cursor',
    children: [
      {
        name: 'Buttons',
        url: '/buttons/buttons',
        icon: 'icon-cursor'
      },
      {
        name: 'Dropdowns',
        url: '/buttons/dropdowns',
        icon: 'icon-cursor'
      },
      {
        name: 'Brand Buttons',
        url: '/buttons/brand-buttons',
        icon: 'icon-cursor'
      }
    ]
  },
  {
    name: 'Hồ sơ ứng tuyển',
    url: '/charts',
    icon: 'icon-pie-chart',
    children: [
      {
        name: 'Buttons',
        url: '/buttons/buttons',
        icon: 'icon-cursor'
      },
      {
        name: 'Dropdowns',
        url: '/buttons/dropdowns',
        icon: 'icon-cursor'
      },
      {
        name: 'Brand Buttons',
        url: '/buttons/brand-buttons',
        icon: 'icon-cursor'
      },
    ]
  },
  {
    name: 'Test',
    url: '/test',
    icon: 'icon-speedometer',
  },
  {
    divider: true
  },
];
