import { UmbEntryPointOnInit } from '@umbraco-cms/backoffice/extension-api';

// load up the manifests here.
import { manifests as dashboardManifests } from './dashboards/manifest.ts';
import { manifests as contextManifests }  from './context/manifest.ts';
import { manifests as sectionManifests } from './section/manifest.ts';
import { manifests as sidebarAppManifests} from './sidebar/manifest.ts';
import { manifests as workspaceManifest} from './workspace/manifest.ts';
import { manifests as documentManifests } from './documents/manifest.ts';
import { manifests as headerManifests } from './header/manifest.ts';
import { manifests as localizationManifests } from './lang/manifest.ts';
import { manifests as workspaceActionManifests} from './actions/workspace/manifest.ts';
import { manifests as entityActionManifests } from './actions/entity/manifest.ts';

import { manifests as styledTextManifests} from './editor/manifest.ts';

export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
    
    // register them here. 
    extensionRegistry.registerMany([
        ...contextManifests,
        ...dashboardManifests,
        ...sectionManifests,
        ...sidebarAppManifests,
        ...workspaceManifest,        
        ...documentManifests,
        ...headerManifests,
        ...localizationManifests,
        ...workspaceActionManifests,
        ...entityActionManifests,
        ...styledTextManifests
    ]);
};
