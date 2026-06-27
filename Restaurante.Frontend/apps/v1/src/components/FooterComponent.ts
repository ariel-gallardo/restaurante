import { TRANSLATIONS } from '@org/shared-shell';

class FooterController {
    static $inject = ['$ngRedux'];
    private unsubscribeRedux: (() => void) | null = null;
    public currentLanguage: 'es' | 'en' = 'es';

    constructor(private $ngRedux: any) {
        const mapStateToThis = (state: any) => ({
            currentLanguage: state?.orderState?.language || 'es'
        });
        this.unsubscribeRedux = this.$ngRedux.connect(mapStateToThis)(this);
    }

    public get t() {
        const lang = this.currentLanguage || 'es';
        return TRANSLATIONS[lang] || TRANSLATIONS.es;
    }

    public get currentYear() {
        return new Date().getFullYear();
    }

    public $onDestroy() {
        if (this.unsubscribeRedux) {
            this.unsubscribeRedux();
        }
    }
}

export default {
    templateUrl: '/components/views/FooterComponent.html5',
    controllerAs: 'ctrl',
    controller: FooterController
};
