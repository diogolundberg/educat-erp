<template>
  <Modal
    v-if="url"
    class="fit-height"
    @hide="url = null">
    <img
      v-if="url.match('(jpe?g|png)$')"
      :src="url"
      class="col-12 fit">
    <iframe
      v-else
      :src="url"
      height="600"
      class="col-12 fit border-none" />
  </Modal>
</template>

<script>
  export default {
    name: "ThumbModal",
    props: {
      url: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
