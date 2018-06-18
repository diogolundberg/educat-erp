<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']"
      post
      @success="$emit('success')">
      <template slot-scope="{ item, errors, submit }">
        <AlertGroup
          :items="item.pendencies"
          text-key="description"
          @click="submit" />
        <Fieldset title="Informações Gerais">
          <InfoBox
            label="Curso"
            value="Medicina" />
          <InfoBox
            label="Turno"
            value="Integral" />
          <InfoBox
            label="Forma de Ingresso"
            value="Vestibular" />
          <InfoBox
            label="Data de Ingresso"
            value="01/02/2018" />
        </Fieldset>
        <Fieldset title="Matriz Curricular">
          <List
            :show-filter="false"
            :per-page="40"
            :columns="[
              {name:'series', title: 'Série'},
              {name:'class', title: 'Disciplinas', css: 'col-5'},
              {name:'theory', title: 'Teórica'},
              {name:'practice', title: 'Prática'},
              {name:'total', title: 'Total'},
            ]"
            :value="[
              {series:'1',class:'Anatomia I',theory:80,practice:80,total:160},
              {series:'1',class:'Anatomia II',theory:80,practice:80,total:160},
              {series:'1',class:'Biofísica I',theory:40,practice:'-',total:40},
              {series:'1',class:'Biofísica II',theory:72,practice:'-',total:72},
              {series:'1',class:'Bioquímica I',theory:80,practice:'-',total:80},
              {series:'1',class:'Bioquímica II',theory:40,practice:40,total:80},
              {series:'1',class:'Citologia',theory:80,practice:80,total:160},
              {series:'1',class:'Embriologia Humana',theory:80,practice:80,total:160},
              {series:'1',class:'Genética',theory:72,practice:72,total:144},
              {series:'1',class:'Histologia I',theory:40,practice:40,total:80},
              {series:'1',class:'Histologia I',theory:40,practice:40,total:80},
              {series:'1',class:'História da Medicina I',theory:72,practice:72,
               total:144},
              {series:'1',class:'Medicina Preventiva I',theory:80,practice:80,
               total:160},
              {series:'1',class:'Metodologia Científica',theory:80,practice:80,
               total:160},
              {series:'1',class:'Práticas em Saúde Coletiva',theory:72,practice:72,
               total:144},
          ]" />
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "InfoForm",
    props: {
      id: {
        type: [String, Number],
        required: true,
      },
    },
    computed: {
      baseUrl() {
        const { onboardingEndpoint } = this.$store.getters;
        return `${onboardingEndpoint}/v2/CourseSummaries`;
      },
    },
  };
</script>
