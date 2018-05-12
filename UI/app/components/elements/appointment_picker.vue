<template>
  <div class="flex m3">
    <VueDatepicker
      v-model="date"
      :disabled-dates="{ customPredictor: isDisabled }"
      class="m2"
      inline
    />
    <List
      v-model="hours"
      :show-filter="false"
      :per-page="10"
      :columns="[
        {name:'id', title: ''},
        {name:'hour', title: 'Horário', css: 'col-10'},
      ]"
      class="ml4"
      @click="$emit('input', $event.id)">
      <template
        slot="column-id"
        scope="{ row }">
        <Radio
          :value="value === row.id"
          class="mr2" />
      </template>
    </List>
  </div>
</template>

<script>
  import VueDatepicker from "vuejs-datepicker";
  import moment from "moment";

  export default {
    name: "AppointmentPicker",
    components: { VueDatepicker },
    props: {
      value: {
        type: Number,
        required: false,
        default: null,
      },
      dates: {
        type: Array,
        default: () => [],
        required: false,
      },
    },
    data() {
      return {
        date: null,
      };
    },
    computed: {
      hours() {
        return this.findDates(this.date);
      },
    },
    methods: {
      isDisabled(date) {
        return this.findDates(date).length === 0;
      },
      findDates(date) {
        const formattedDate = moment(date).format("DD/MM/YYYY");
        return this.dates.filter(a => a.date === formattedDate);
      },
    },
  };
</script>

<style type="styl">
  .vdp-datepicker__calendar {
    border: none !important;
  }
</style>
