<template>
  <div>
    <h2
      v-if="title">
      {{ title }}
    </h2>
    <slot
      :item="item"
      :errors="errors" />
    <div class="right-align">
      <Btn
        submit
        primary
        label="Salvar"
        @click="submitForm" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "DataForm",
    props: {
      title: {
        type: String,
        required: false,
        default: null,
      },
    },
    data() {
      return {
        item: {},
        errors: {},
      };
    },
    mounted() {

    },
    methods: {
      submitForm() {

      },
    },
  };
</script>
