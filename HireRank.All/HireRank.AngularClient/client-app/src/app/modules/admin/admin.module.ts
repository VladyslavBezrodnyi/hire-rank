import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminProfileComponent } from './admin-profile/admin-profile.component';
import {AdminRoutingModule} from './admin-routing.module';
import {NgZorroAntdModule} from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AdminCampaignsComponent } from './admin-campaigns/admin-campaigns.component';
import { NzFilterTriggerComponent, NzTableModule } from 'ng-zorro-antd/table';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { IconsProviderModule } from 'src/app/icons-provider.module';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NZ_ICONS } from 'ng-zorro-antd/icon';
import { NZ_I18N, en_US, uk_UA } from 'ng-zorro-antd/i18n';
import { IconDefinition } from '@ant-design/icons-angular';
import * as AllIcons from '@ant-design/icons-angular/icons';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { CreateCampaignComponent } from './create-campaign/create-campaign.component';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { EditCampaignComponent } from './edit-campaign/edit-campaign.component';


const antDesignIcons = AllIcons as {
  [key: string]: IconDefinition;
};
const icons: IconDefinition[] = Object.keys(antDesignIcons).map(key => antDesignIcons[key])

@NgModule({
  declarations: [AdminProfileComponent, AdminCampaignsComponent, CreateCampaignComponent, EditCampaignComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgZorroAntdModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzDropDownModule,
    IconsProviderModule,
    NzIconModule,
    NzDatePickerModule,
    NzMessageModule,
    NzPopconfirmModule
  ],
  providers:[{ provide: NZ_I18N, useValue: en_US }, { provide: NZ_ICONS, useValue: icons }]
})
export class AdminModule { }
