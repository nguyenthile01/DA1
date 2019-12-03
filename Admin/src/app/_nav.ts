import { INavData } from "@coreui/angular";

export const navItems: INavData[] = [
  {
    name: "Dashboard",
    url: "/dashboard",
    icon: "icon-speedometer",
    badge: {
      variant: "info",
      text: "NEW"
    }
  },

  {
    title: true,
    name: "Danh mục"
  },

  {
    name: "buttons",
    url: "/buttons",
    icon: "icon-cursor",
    children: [
      {
        name: "Buttons",
        url: "/buttons/buttons",
        icon: "icon-cursor"
      },
      {
        name: "Dropdowns",
        url: "/buttons/dropdowns",
        icon: "icon-cursor"
      },
      {
        name: "Brand Buttons",
        url: "/buttons/brand-buttons",
        icon: "icon-cursor"
      }
    ]
  },
  {
    name: "User",
    url: "/user",
    icon: "fa fa-user-o ",
    children: [
      {
        name: "Admin",
        url: "/user/admin",
        icon: "fa fa-angle-right"
      },
      {
        name: "Employer",
        url: "/user/employer",
        icon: "fa fa-angle-right"
      },
      {
        name: "JobSeeker",
        url: "/user/jobSeeker",
        icon: "fa fa-angle-right"
      }
    ]
  },
  {
    name: "Job Manager",
    url: "/job",
    icon: "fa fa-columns",
    children: [
      {
        name: "Category Job",
        url: "/job/category",
        icon: "icon-puzzle"
      },
      {
        name: "Post",
        url: "/job/post",
        icon: "icon-puzzle"
      }
    ]
  },
  {
    name: "Position",
    url: "/position",
    icon: "fa fa-map-marker"
  },
  // {
  //   name: 'base',
  //   url: '/base',
  //   icon: 'bank (alias)',
  //   children: [
  //     {
  //       name: 'Cards',
  //       url: '/base/cards',
  //       icon: 'icon-puzzle'
  //     },
  //     {
  //       name: 'Carousels',
  //       url: '/base/paginations',
  //       icon: 'icon-puzzle'
  //     },
  //     {
  //       name: 'Collapses',
  //       url: '/base/collapses',
  //       icon: 'icon-puzzle'
  //     },
  //     {
  //       name: 'Forms',
  //       url: '/base/forms',
  //       icon: 'icon-puzzle'
  //     },
  //     {
  //       name: 'Table',
  //       url: '/base/tables',
  //       icon: 'icon-puzzle'
  //     },
  //   ]
  // },
  // {
  //   name: 'Buttons',
  //   url: '/charts',
  //   icon: 'icon-pie-chart',
  //   children: [
  //     {
  //       name: 'Buttons',
  //       url: '/buttons/buttons',
  //       icon: 'icon-cursor'
  //     },
  //     {
  //       name: 'Dropdowns',
  //       url: '/buttons/dropdowns',
  //       icon: 'icon-cursor'
  //     },
  //     {
  //       name: 'Brand Buttons',
  //       url: '/buttons/brand-buttons',
  //       icon: 'icon-cursor'
  //     },
  //   ]
  // },
  {
    divider: true
  }
];
